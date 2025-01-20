// Ignore Spelling: Utc

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ImportTournamentDTOs
{
    public class ImportTournamentDTO
    {
        public required string TournamentName { get; set; }

        public DateTime? DateStartUtc { get; set; }

        public DateTime? DateUtc { get; set; }
        public int? Date_StartFuzzyUtc { get; set; }
        public string? League { get; set; }

        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? Split { get; set; }
        public string? TournamentLevel {  get; set; }   
        public bool IsQualifier { get; set; } 
        public string? AlternativeNames {  get; set; }

    }
}
