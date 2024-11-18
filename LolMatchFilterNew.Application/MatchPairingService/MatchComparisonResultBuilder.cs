using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Application.MatchPairingService.MatchComparisonResults;

namespace LolMatchFilterNew.Application.MatchPairingService
{
    public class MatchComparisonResultBuilder
    {
        private readonly MatchComparisonResult _result = new();
        private bool _teamsSet;
        private bool _youtubeInfoSet;
        private bool _matchDateSet;
    }
}
