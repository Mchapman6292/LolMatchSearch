﻿

namespace Domain.DTOs.TeamnameDTOs
{
    public class TeamnameDTO
    {
        public string? Longname { get; set; }

        public string? Short { get; set; }

        public string? Medium { get; set; }

        // Inputs in database need to be trimmed & formatted correctly to remove quotation marks etc, example: {"1 trick ponies;1tp"}
        // This is done with TeamnameDTOBuilder PopulateTeamNamesAndAbbreviations
        public List<string>? FormattedInputs { get; set; }
    }
}
