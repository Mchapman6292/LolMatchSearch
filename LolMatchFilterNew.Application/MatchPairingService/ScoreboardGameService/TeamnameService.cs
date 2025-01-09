

// Ignore Spelling: Youtube

using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.ITeamNameServices;
using Domain.DTOs.Western_MatchDTOs;
using Domain.DTOs.Processed_YoutubeDataDTOs;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;

namespace Application.MatchPairingService.ScoreboardGameService.MatchDTOServices.TeamNameServices
{
    public class TeamNameService : ITeamNameService
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IStoredSqlFunctionCaller _sqlFunctionCaller;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;

        private readonly List<WesternMatchDTO> _westernMatches;
        private readonly Dictionary<string, string> _shortNames;
        private readonly Dictionary<string, string> _mediumNames;
        private readonly Dictionary<string, List<string>> _inputs;


        public TeamNameService(IAppLogger appLogger, IObjectLogger objectLogger, IStoredSqlFunctionCaller testFunction, IImport_YoutubeDataRepository import_YoutubeDataRepository)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _sqlFunctionCaller = testFunction;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;


            _westernMatches = _sqlFunctionCaller.GetWesternMatches()
              .GetAwaiter()
              .GetResult();


            _shortNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _mediumNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _inputs = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        }


        public void PopulateTeamVariations(IEnumerable<TeamnameDTO> teamNames)
        {
            int count = 0;
            foreach (var team in teamNames)
            {
                if (string.IsNullOrEmpty(team.LongName))
                    continue;

                if (!string.IsNullOrEmpty(team.Short))
                {
                    _shortNames.Add(team.LongName, team.Short);
                }

                if (!string.IsNullOrEmpty(team.Medium))
                {
                    _mediumNames.Add(team.LongName, team.Medium);
                }

                if (team.FormattedInputs != null && team.FormattedInputs.Count > 0)
                {
                    _inputs.Add(team.LongName, team.FormattedInputs);
                }

                count++;
            }

            _appLogger.Info($"{nameof(PopulateTeamVariations)} complete total count: {count}");
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
