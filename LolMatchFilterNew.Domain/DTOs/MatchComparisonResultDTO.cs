using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs
{
    public class MatchComparisonResultDTO
    {
        public MatchComparisonResultDTO()
        { }
        public string LeaguepediaGameId { get; set; }
        public DateTime? MatchDate { get; set; }
        public string LeaguepediaTeam1 { get; set; }
        public string LeaguepediaTeam2 { get; set; }
        public string YoutubeVideoId { get; set; }
        public string YoutubeTitle { get; set; }
        public DateTime? YoutubePublishDate { get; set; }

        // Properties set during team extraction
        public string? YoutubeTeam1 { get; set; }
        public string? YoutubeTeam2 { get; set; }
        public string? ExtractedTeamInfo { get; set; }

        // Result properties
        public bool? DoesMatch { get; set; }
        public string? MismatchReason { get; set; }
    }
}
