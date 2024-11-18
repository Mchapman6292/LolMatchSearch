using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Application.MatchPairingService
{
    public class MatchComparisonResult
    {
        public DateTime? MatchDate { get; set; }
        public string? LeaguepediaGameId { get; set; }
        public string? Team1 { get; set; }
        public string? Team2 { get; set; }
        public string? YoutubeVideoId { get; set; }
        public string? YoutubeTitle { get; set; }

        public string? ExtractedTeamInfo { get; set; }
        public DateTime? YoutubePublishDate { get; set; }
        public bool TeamsMatch { get; set; }
        public string? MismatchReason { get; set; }



    }
}
