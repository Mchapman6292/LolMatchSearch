using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using System.Drawing;
using System.Text.RegularExpressions;
using LolMatchFilterNew.Domain.DTOs.YoutubeVideoResults;
using Xceed.Document.NET;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchSearches;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;


namespace LolMatchFilterNew.Application.MatchPairingService.MatchSearch
{
    public class MatchSearch : IMatchSearch
    {
        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaMatchDetailRepository _leaguepediaMatchDetailRepository;
        private readonly IApiHelper _apiHelper;


        // Looks for names seperated by |


        public MatchSearch(IAppLogger appLogger, ILeaguepediaMatchDetailRepository leaguepediaMatchDetailRepository, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _leaguepediaMatchDetailRepository = leaguepediaMatchDetailRepository;
            _apiHelper = apiHelper;
        }




        public async Task<List<string>> ExtractEndTeamStringForMultiple(List<YoutubeVideoEntity> youtubeVideos)
        {
            List<(string, string)> missingSeperatorResults = new List<(string, string)>();

            List<string> ExtractedEndStrings = new List<string>();

            if (youtubeVideos.Count == 0)
            {
                _appLogger.Error($"No YoutubeEntities found for parameter in {nameof(ExtractEndTeamStringForMultiple)}.");
                throw new ArgumentException("No YoutubeEntities found for parameter");
            }

            foreach (var video in youtubeVideos)
            {
                var title = video.VideoTitle;
                var videoId = video.YoutubeVideoId;

                int lastSeperatorIndex = title.LastIndexOf('|');
                if (lastSeperatorIndex == -1)
                {
                    var tuple = (title, videoId);
                    missingSeperatorResults.Add(tuple);
                }

                string extractedResult = title.Substring(lastSeperatorIndex + 1).Trim();

                ExtractedEndStrings.Add(extractedResult);
            }
            if (missingSeperatorResults.Count > 0)
            {
                var currentDateTime = DateTime.UtcNow;
                string title = "MissingSeperatorResults" + currentDateTime.ToString();
                await _apiHelper.WriteListDictToWordDocAsync(missingSeperatorResults, title);
            }
            return ExtractedEndStrings;
        }



        public async Task<string> ExtractEndTeamString(YoutubeVideoEntity youtubeVideo)
        {
            if (youtubeVideo == null)
            {
                _appLogger.Error($"No YoutubeEntity provided for parameter in {nameof(ExtractEndTeamString)}.");
                throw new ArgumentException("YoutubeEntity parameter is null");
            }

            var title = youtubeVideo.VideoTitle;
            var videoId = youtubeVideo.YoutubeVideoId;

            int lastSeparatorIndex = title.LastIndexOf('|');
            if (lastSeparatorIndex == -1)
            {
                var missingSeparatorResult = (title, videoId);
                var currentDateTime = DateTime.UtcNow;
                string documentTitle = "MissingSeperatorResult" + currentDateTime.ToString();

                await _apiHelper.WriteListDictToWordDocAsync(new List<(string, string)> { missingSeparatorResult }, documentTitle);

                _appLogger.Warning($"Missing separator '|' in video title: {title}. Result logged.");
                throw new InvalidOperationException($"The title '{title}' does not contain a valid separator '|'.");
            }

            return title.Substring(lastSeparatorIndex + 1).Trim();
        }


        public async Task CheckYoutubeTitleForVsTeams(YoutubeVideoEntity youtubeVideoEntity)
        {

        }




    }

}


