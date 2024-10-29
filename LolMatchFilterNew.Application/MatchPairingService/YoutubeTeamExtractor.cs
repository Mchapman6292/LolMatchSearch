using LolMatchFilterNew.Domain.DTOs.YoutubeVideoResult;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LolMatchFilterNew.Application.MatchComparisonResults;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;

namespace LolMatchFilterNew.Application.MatchPairingService.YoutubeTeamExtractors
{
    public  class YoutubeTeamExtractor : IYoutubeTeamExtractor
    {
        // (?:^|[^a-zA-Z0-9]) - Start of string or non-alphanumeric character (non-capturing)
        // ([A-Z0-9][A-Za-z0-9\s]*?) - Team name starting with capital letter or number, followed by letters, numbers, spaces
        // \s+(?:[vV][sS]|versus)\s+ - "vs" or "versus" with surrounding spaces
        // ([A-Z0-9][A-Za-z0-9\s]*?) - Second team name with same pattern
        // (?=\s*(?:$|[^a-zA-Z0-9]|Game|Match|Highlights|ALL)) - Followed by end of string or specific keywords

        private readonly Regex TeamNamePattern = new(
              @"(?:^|[^a-zA-Z0-9])([A-Z0-9][A-Za-z0-9\s]*?)\s+(?:[vV][sS]|versus)\s+([A-Z0-9][A-Za-z0-9\s]*?)(?=\s*(?:$|[^a-zA-Z0-9]|Game|Match|Highlights|ALL))",
              RegexOptions.Compiled);


        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaMatchDetailRepository _leaguepediaMatchDetailRepository;
        private readonly IApiHelper _apiHelper;

        public YoutubeTeamExtractor(IAppLogger appLogger, ILeaguepediaMatchDetailRepository leaguepediaMatchDetailRepository, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _leaguepediaMatchDetailRepository = leaguepediaMatchDetailRepository;
            _apiHelper = apiHelper;
        }

        public bool MatchVsPatternAndUpdateMatchComparisonResultEntity(YoutubeVideoEntity youtubeVideo, MatchComparisonResult comparisonResult)
        {
            var match = TeamNamePattern.Match(youtubeVideo.Title);

            if(match.Success)
            {
                comparisonResult.Team1 = match.Groups[1].Value;
                comparisonResult.Team2 = match.Groups[2].Value;
                return true;
            }
            else
            {

                comparisonResult.Team1 = string.Empty;
                comparisonResult.Team2 = string.Empty;
                return true;
            }
        }
    }
}
