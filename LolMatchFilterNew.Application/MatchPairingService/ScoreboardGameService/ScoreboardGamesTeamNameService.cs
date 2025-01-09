

// Ignore Spelling: Youtube

using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IScoreboardGamesTeamNameServices;
using Domain.DTOs.Western_MatchDTOs;
using Domain.DTOs.Processed_YoutubeDataDTOs;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using Application.MatchPairingService.ScoreboardGameService.TeamnameDTOBuilders;

namespace Application.MatchPairingService.ScoreboardGameService.MatchDTOServices.TeamNameServices.ScoreboardGamesTeamNameServices
{
    public class ScoreboardGamesTeamNameService : IScoreboardGamesTeamNameService
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IStoredSqlFunctionCaller _sqlFunctionCaller;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IImport_TeamnameRepository _teamnameRepository;
        private readonly ITeamNameDTOBuilder _teamNameDTOBuilder;

        public List<TeamnameDTO> TeamNamesAndAbbreviations = new List<TeamnameDTO>();

        private readonly List<WesternMatchDTO> _westernMatches;

        private readonly Dictionary<string, string> _shortNames;
        private readonly Dictionary<string, string> _mediumNames;
        private readonly Dictionary<string, List<string>> _inputs;


        public ScoreboardGamesTeamNameService(IAppLogger appLogger, IObjectLogger objectLogger, IStoredSqlFunctionCaller testFunction, IImport_YoutubeDataRepository import_YoutubeDataRepository, IImport_TeamnameRepository teamNameRepository, ITeamNameDTOBuilder teamNameDTOBuilder)
        {

            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _sqlFunctionCaller = testFunction;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _teamnameRepository = teamNameRepository;
            _teamNameDTOBuilder = teamNameDTOBuilder;

 


            _westernMatches = _sqlFunctionCaller.GetWesternMatches()
              .GetAwaiter()
              .GetResult();


        }

        // Retrieves all team names from repository and transforms them into DTOs, storing them in TeamNamesAndAbbreviations property.
        // Inputs in database need to be trimmed & formatted correctly to remove quotation marks etc, example: {"1 trick ponies;1tp"}
        public async Task PopulateTeamNamesAndAbbreviations()
        {
            var teamnames = await _teamnameRepository.GetAllTeamnamesAsync();
            TeamNamesAndAbbreviations = teamnames.Select(t => _teamNameDTOBuilder.BuildTeamnameDTO(
                t.TeamnameId,
                t.Longname,
                t.Short,
                t.Medium,
                t.Inputs

            )).ToList();

            _appLogger.Info($"TeamNamesAndAbbreviations count: {TeamNamesAndAbbreviations.Count}");
        }


        public async Task TESTLogTeamNameAbbreviations()
        {
            await PopulateTeamNamesAndAbbreviations();

            var firstTeamDTO = TeamNamesAndAbbreviations.FirstOrDefault();


            var lastTeamDTO = TeamNamesAndAbbreviations.LastOrDefault();

            _objectLogger.LogTeamnameDTO(firstTeamDTO);
            _objectLogger.LogTeamnameDTO(lastTeamDTO);

        }







        public List<TeamnameDTO> GetTeamNamesAndAbbreviations()
        {
            if (TeamNamesAndAbbreviations == null || !TeamNamesAndAbbreviations.Any())
            {
                throw new ArgumentNullException(nameof(TeamNamesAndAbbreviations), "The list is null or empty.");
            }
            return TeamNamesAndAbbreviations;
        }



        public HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<Processed_YoutubeDataDTO> processed_YoutubeDataDTOs)
        {
            HashSet<string> distinctTeamNames = new HashSet<string>();

            int distinctCount = 0;

            foreach (var dto in processed_YoutubeDataDTOs)
            {
                if (!string.IsNullOrEmpty(dto.Team1))
                {
                    distinctTeamNames.Add(dto.Team1);
                    distinctCount++;
                }
                if (!string.IsNullOrEmpty(dto.Team2))
                {
                    distinctTeamNames.Add(dto.Team2);
                    distinctCount++;
                }
            }
            _appLogger.Info($"Total number of distinct teams for {nameof(GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO)}: {distinctCount}.");

            return distinctTeamNames;
        }


        public void TESTCheckAllProcessedEuAndNaAgainstKnownAbbreviations(HashSet<string> distinctTeamNames, List<TeamnameDTO> teamNameDTOs)
        {
            int matchCount = 0;
            int missCount = 0;

            foreach (string teamName in distinctTeamNames)
            {
                bool foundMatch = false;

                foreach (var dto in teamNameDTOs)
                {
                    if (IsTeamNameMatch(teamName, dto))
                    {
                        matchCount++;
                        foundMatch = true;
                        break;
                    }
                }

                if (!foundMatch)
                {
                    missCount++;
                    _appLogger.Info($"No match found for team: {teamName}");
                }
            }

            _appLogger.Info($"Total matches: {matchCount}, Total misses: {missCount}");
        }

        private bool IsTeamNameMatch(string teamName, TeamnameDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.LongName) && teamName.Equals(dto.LongName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (!string.IsNullOrEmpty(dto.Medium) && teamName.Equals(dto.Medium, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (!string.IsNullOrEmpty(dto.Short) && teamName.Equals(dto.Short, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (dto.FormattedInputs?.Contains(teamName, StringComparer.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            return false;
        }

















    }
}
