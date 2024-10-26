using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Application.MatchComparison;
using System.Drawing;
using System.Text.RegularExpressions;
using LolMatchFilterNew.Domain.DTOs.YoutubeVideoResult;
using Xceed.Document.NET;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;


namespace LolMatchFilterNew.Application.MatchPairingService.MatchSearch
{
    public class MatchSearch
    {
        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaMatchDetailRepository _leaguepediaMatchDetailRepository;
        private readonly IApiHelper _apiHelper;



        public MatchSearch(IAppLogger appLogger, ILeaguepediaMatchDetailRepository leaguepediaMatchDetailRepository, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _leaguepediaMatchDetailRepository = leaguepediaMatchDetailRepository;
            _apiHelper = apiHelper;
        }


        public (string, string) CompareTeamNames(string youtubeTitle, List<string>[] leaguepediaTeams)
        {

            var lastSeperatorIndex = youtubeTitle.LastIndexOf('|');
            string extractedEnding = lastSeperatorIndex => 0
                 ? youtubeTitle.Substring(lastSeparatorIndex + 1).Trim()
                    : youtubeTitle.Trim();

        }


        public async Task<List<string>> GetTeamsIfTitleIncludesVs(List<YoutubeVideoEntity> youtubeVideos)
        {

        }
            
          

        public async Task<List<string>> ExtractEndTeamStringFromYoutubeTitle(List<YoutubeVideoEntity> youtubeVideos)
        {
            List<(string, string)> missingSeperatorResults = new List<(string, string)>();

            List<string> ExtractedEndStrings = new List<string>();

            if(youtubeVideos.Count == 0)
            {
                _appLogger.Error($"No YoutubeEntities found for parameter in {nameof(ExtractEndTeamStringFromYoutubeTitle)}.");
                throw new ArgumentException("No YoutubeEntities found for parameter");
            }

            foreach (var video in youtubeVideos)
            {
                var title = video.Title;
                var videoId = video.VideoId;

                int lastSeperatorIndex = title.LastIndexOf('|');
                if (lastSeperatorIndex == -1)
                {
                    var tuple = (title, videoId);
                    missingSeperatorResults.Add(tuple);
                }

                string extractedResult = title.Substring(lastSeperatorIndex + 1).Trim();

                ExtractedEndStrings.Add(extractedResult);
            }
            if(missingSeperatorResults.Count > 0) 
            {
                var currentDateTime = DateTime.UtcNow;
                string title = "MissingSeperatorResults" + currentDateTime.ToString();
                await _apiHelper.WriteListDictToWordDocAsync(missingSeperatorResults, title);
            }
            return ExtractedEndStrings;   
            }
        }






    }


