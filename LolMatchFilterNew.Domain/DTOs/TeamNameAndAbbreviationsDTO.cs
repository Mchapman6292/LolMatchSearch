using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TeamNameAndAbbreviationsDTOs
{
    public class TeamNameAndAbbreviationsDTO
    {
        public string TeamNameId { get; set; }
        public string? LongName { get; set; }
        public string? Medium { get; set; }

        public string? Short { get; set; }

        public List<string>? FormattedInputs { get; set; }

    }
}
