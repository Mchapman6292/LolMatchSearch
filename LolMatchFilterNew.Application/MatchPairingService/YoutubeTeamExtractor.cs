using LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        // Looks for team names either side of vs / versus, not case sensitive. 
        public List<string> MatchVsPatternAndUpdateMatchComparisonResultEntity(string youtubeTitle)
        {
            var match = TeamNamePattern.Match(youtubeTitle);

            if(match.Success)
            {
                string Team1 = match.Groups[1].Value;
                string Team2 = match.Groups[2].Value;

                return new List<string> { Team1, Team2 };
            }
            else
            {
                return null;
            }
        }


    }
}
