// Ignore Spelling: Utc

namespace Domain.DTOs.ImportTournamentDTOs
{
    public class ImportTournamentDTO
    {
        public required string TournamentName { get; set; }

        public DateTime? DateStartUtc { get; set; }

        public DateTime? DateUtc { get; set; }
        public string? League { get; set; }

        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? Split { get; set; }
        public string? TournamentLevel {  get; set; }   
        public bool IsQualifier { get; set; } 
        public string? AlternativeNames {  get; set; }

    }
}
