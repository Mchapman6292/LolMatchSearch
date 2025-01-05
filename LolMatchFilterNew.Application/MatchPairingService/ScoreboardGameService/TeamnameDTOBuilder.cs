using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.Interfaces.ApplicationInterfaces.ITeamnameDTOBuilders;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.DTOs.Western_MatchDTOs;
using System.Linq;

namespace Application.MatchPairingService.ScoreboardGameService.TeamnameDTOBuilders
{
    public class TeamnameDTOBuilder  :ITeamnameDTOBuilder
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_TeamnameRepository _teamnameRepository;
        private readonly IObjectLogger _objectLogger;
        private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
        private List<TeamnameDTO> TeamNamesAndAbbreviations {  get; set; } 


        public TeamnameDTOBuilder(IAppLogger appLogger, IImport_TeamnameRepository teamnameRepository, IObjectLogger objectLogger, IStoredSqlFunctionCaller storedSqlFunctionCaller)
        {
            _appLogger = appLogger;
            _teamnameRepository = teamnameRepository;
            TeamNamesAndAbbreviations = new List<TeamnameDTO>();    
            _objectLogger = objectLogger;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
        }


        public List<TeamnameDTO> GetTeamNamesAndAbbreviations()
        {
            return TeamNamesAndAbbreviations;
        }



        // Retrieves all teamnames from repository and transforms them into DTOs, storing them in TeamNamesAndAbbreviations property.
        // Inputs in database need to be trimmed & formatted correctly to remove quotation marks etc, example: {"1 trick ponies;1tp"}
        public async Task PopulateTeamNamesAndAbbreviations()
        {
            var teamnames = await _teamnameRepository.GetAllTeamnamesAsync();
            TeamNamesAndAbbreviations = teamnames.Select(t => BuildTeamnameDTO(
                t.Longname,
                t.Short,
                t.Medium,
                t.Inputs
               
            )).ToList();

            _appLogger.Info($"TeamNamesAndAbbreviations count: {TeamNamesAndAbbreviations.Count}");
        }


        public TeamnameDTO BuildTeamnameDTO(string? longname, string? mediumName, string? shortname, List<string>? inputs) 
        {
            return new TeamnameDTO
            {
                Longname = longname,
                Medium = mediumName ?? string.Empty,
                Short = shortname ?? string.Empty,
                FormattedInputs = ParseTeamnameInputsColumn(inputs)
            };    
        }



        // Parses list of teamname inputs from format {"name1;name2"} to ["name1", "name2"]
        private List<string>? ParseTeamnameInputsColumn(List<string>? inputs)
        {
            if (inputs == null || !inputs.Any()) return null;

            return inputs.SelectMany(input => input
                            .Trim('{', '}', '"')
                            .Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(name => name.Trim()))
                            .ToList();
        }







        public async Task TESTLogTeamNameAbbreviations()
        {
            await PopulateTeamNamesAndAbbreviations();

            var firstTeamDTO = TeamNamesAndAbbreviations.FirstOrDefault();


            var lastTeamDTO = TeamNamesAndAbbreviations.LastOrDefault();

            _objectLogger.LogTeamnameDTO(firstTeamDTO);
            _objectLogger.LogTeamnameDTO(lastTeamDTO);

        }



        public async Task<List<TeamnameDTO>> BuildTeamnameDTOFromGetWesternMatches(List<WesternMatchDTO> westernMatches)
        {


            List<TeamnameDTO> teamnames = new List<TeamnameDTO>();

            int count = 0;

            foreach(var match in westernMatches)
            {
                bool team1Exisits = teamnames.Any(t => t.TeamNameId == match.Team1Team_Id);

                if(!team1Exisits)
                {
                    teamnames.Add(BuildTeamnameDTO(match.Team1_Longname, match.Team1_Medium, match.Team1_Short, match.Team1_Inputs));
                }

                bool team2Exists = teamnames.Any(t => t.TeamNameId == match.Team2Team_Id);

                if(!team2Exists)
                {
                    teamnames.Add(BuildTeamnameDTO(match.Team2_Longname, match.Team2_Medium, match.Team2_Short, match.Team2_Inputs));
                }
                count++;
            }
            _appLogger.Info($"{nameof(BuildTeamnameDTO)} complete, TeamnameDTO count: {teamnames.Count}.");
            return teamnames;
        }





    }
}
