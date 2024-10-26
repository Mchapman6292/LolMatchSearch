using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.DTOs.MatchComparisonResults
{
    public class MatchComparisonResult
    {
        // Required properties initialized in constructor
        public DateTime? MatchDate { get; private set; }
        public string LeaguepediaGameId { get; private set; }
        public string LeaguepediaTeam1 { get; private set; }
        public string LeaguepediaTeam2 { get; private set; }
        public string YoutubeVideoId { get; private set; }
        public string YoutubeTitle { get; private set; }
        public DateTime? YoutubePublishDate { get; private set; }

        // Properties set during team extraction
        public string? YoutubeTeam1 { get; private set; }
        public string? YoutubeTeam2 { get; private set; }
        public string? ExtractedTeamInfo { get; private set; }


        // Result properties
        public bool? DoesMatch { get; private set; }
        public string? MismatchReason { get; private set; }

    }
}
