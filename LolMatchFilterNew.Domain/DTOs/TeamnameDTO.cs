

namespace Domain.DTOs.TeamnameDTOs
{
    public class TeamnameDTO
    {
        public string? TeamNameId { get; set; } 
        public string? Longname { get; set; }

        

        public string? Medium { get; set; }

        public string? Short { get; set; }


        // Inputs in database need to be trimmed & formatted correctly to remove quotation marks etc, example: {"1 trick ponies;1tp"}
        // This is done with TeamnameDTOBuilder PopulateTeamNamesAndAbbreviations

        public List<string>? FormattedInputs { get; set; }

   
        public bool SharesNameWithOtherTeam {  get; set; }

        // In cases where the team is a duplicate the region is used to separate them.
        public string? Region { get; set; }  
    }
}
