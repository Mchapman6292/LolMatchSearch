using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using Domain.DTOs.TeamnameDTO;

namespace Application.MatchPairingService.ScoreboardGameService.TeamnameDTOBuilders
{
    public class TeamnameDTOBuilder
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_TeamnameRepository _teamnameRepository;
        private List<TeamnameDTO> TeamNamesAndAbbreviations {  get; set; } 


        public TeamnameDTOBuilder(IAppLogger appLogger, IImport_TeamnameRepository teamnameRepository)
        {
            _appLogger = appLogger;
            _teamnameRepository = teamnameRepository;
            TeamNamesAndAbbreviations = new List<TeamnameDTO>();    
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


        public async Task PopulateTeamNamesAndAbbreviations()
        {
            var teamnames = await _teamnameRepository.GetAllTeamnamesAsync();
            TeamNamesAndAbbreviations = teamnames.Select(t => BuildTeamnameDTO(
                t.Longname,
                t.Short,
                t.Medium,
                t.Inputs
            )).ToList();
        }



        public List<TeamnameDTO> GetTeamNamesAndAbbreviations()
        {
            return TeamNamesAndAbbreviations;
        }





    }
}
