

namespace Domain.DTOs.TeamnameDTOs
{
    public class TeamNameDTO
    {
        public string TeamNameId { get; set; } 
        public string? LongName { get; set; }

        public string? MediumName { get; set; }

        public string? ShortName { get; set; }


        // Inputs in database need to be trimmed & formatted correctly to remove quotation marks etc, example: {"1 trick ponies;1tp"}
        // This is done with TeamnameDTOBuilder PopulateTeamNamesAndAbbreviations

        public List<string>? FormattedInputs { get; set; }

    }
}
