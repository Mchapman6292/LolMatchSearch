
namespace Domain.DTOs.Western_MatchDTOs
{
    public class WesternMatchDTO
    {
        public string Game_Id { get; set; } // character varying
        public string Match_Id { get; set; } // character varying
        public DateTime? DateTime_Utc { get; set; } // timestamp with time zone
        public string? Tournament { get; set; } // character varying
        public string? Team1 { get; set; } // character varying
        public string? Team1Team_Id { get; set; }  //character varying
        public string? Team1_Players { get; set; } // character varying
        public string? Team1_Picks { get; set; } // character varying
        public string? Team2 { get; set; } // character varying
        public string? Team2Team_Id { get; set; }  // Character varying
        public string? Team2_Players { get; set; } // character varying
        public string? Team2_Picks { get; set; } // character varying
        public string? Win_Team { get; set; } // character varying
        public string? Loss_Team { get; set; } // character varying
        public string? Team1_Region { get; set; } // character varying
        public string? Team1_Longname { get; set; } // character varying
        public string? Team1_Medium { get; set; } // character varying
        public string? Team1_Short { get; set; } // character varying
        public List<string>? Team1_Inputs { get; set; } // text[]
        public string? Team2_Region { get; set; } // character varying
        public string? Team2_Longname { get; set; } // character varying
        public string? Team2_Medium { get; set; } // character varying
        public string? Team2_Short { get; set; } // character varying
        public List<string>? Team2_Inputs { get; set; } // text[]
    }
}
