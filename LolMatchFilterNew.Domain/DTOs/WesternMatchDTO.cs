using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Western_MatchDTOs
{
    public class WesternMatchDTO
    {
        public string Game_Id { get; set; } // CHARACTER VARYING
        public string Match_Id { get; set; } // CHARACTER VARYING
        public DateTime? DateTime_Utc { get; set; } // TIMESTAMP WITH TIME ZONE
        public string? Tournament { get; set; } // CHARACTER VARYING
        public string? Team1 { get; set; } // CHARACTER VARYING
        public string? Team1_Players { get; set; } // CHARACTER VARYING
        public string? Team1_Picks { get; set; } // CHARACTER VARYING
        public string? Team2 { get; set; } // CHARACTER VARYING
        public string? Team2_Players { get; set; } // CHARACTER VARYING
        public string? Team2_Picks { get; set; } // CHARACTER VARYING
        public string? Win_Team { get; set; } // CHARACTER VARYING
        public string? Loss_Team { get; set; } // CHARACTER VARYING
        public string? Team1_Region { get; set; } // CHARACTER VARYING
        public string? Team1_Longname { get; set; } // CHARACTER VARYING
        public string? Team1_Short { get; set; } // CHARACTER VARYING
        public List<string>? Team1_Inputs { get; set; } // TEXT[]
        public string? Team2_Region { get; set; } // CHARACTER VARYING
        public string? Team2_Longname { get; set; } // CHARACTER VARYING
        public string? Team2_Short { get; set; } // CHARACTER VARYING
        public List<string>? Team2_Inputs { get; set; } // TEXT[]
    }
}
