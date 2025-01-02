using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.Interfaces.ApplicationInterfaces.ITeamnameDTOBuilders;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;

namespace Application.MatchPairingService.ScoreboardGameService.TeamnameDTOBuilders
{
    public class TeamnameDTOBuilder  :ITeamnameDTOBuilder
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_TeamnameRepository _teamnameRepository;
        private readonly IObjectLogger _objectLogger;
        private List<TeamnameDTO> TeamNamesAndAbbreviations {  get; set; } 


        public TeamnameDTOBuilder(IAppLogger appLogger, IImport_TeamnameRepository teamnameRepository, IObjectLogger objectLogger)
        {
            _appLogger = appLogger;
            _teamnameRepository = teamnameRepository;
            TeamNamesAndAbbreviations = new List<TeamnameDTO>();    
            _objectLogger = objectLogger;
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


        public TeamnameDTO BuildTeamnameDTO(string? longname, string? shortname, string? mediumName, List<string>? inputs) 
        {
            return new TeamnameDTO
            {
                Longname = longname,
                Short = shortname,
                Medium = mediumName,
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




        public List<TeamnameDTO> GetTeamNamesAndAbbreviations()
        {
            return TeamNamesAndAbbreviations;
        }



        public async Task TESTLogTeamNameAbbreviations()
        {
            await PopulateTeamNamesAndAbbreviations();

            var firstTeamDTO = TeamNamesAndAbbreviations.FirstOrDefault();


            var lastTeamDTO = TeamNamesAndAbbreviations.LastOrDefault();

            _objectLogger.LogTeamnameDTO(firstTeamDTO);
            _objectLogger.LogTeamnameDTO(lastTeamDTO);



        }





    }
}
