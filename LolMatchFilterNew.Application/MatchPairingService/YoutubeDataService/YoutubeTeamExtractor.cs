using LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using System.Text.RegularExpressions;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.ITeamnameDTOBuilders;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;

namespace Application.MatchPairingService.YoutubeDataService.YoutubeTeamExtractors
{
    public class YoutubeTeamExtractor : IYoutubeTeamExtractor
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
        private readonly IImport_ScoreboardGamesRepository _Import_ScoreboardGamesRepository;
        private readonly IApiHelper _apiHelper;
        private readonly IStoredSqlFunctionCaller _sqlFunctionCaller;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly ITeamnameDTOBuilder _teamnameDTOBuilder;    


        public YoutubeTeamExtractor(

            IAppLogger appLogger,
            IApiHelper apiHelper,
            IImport_ScoreboardGamesRepository import_ScoreboardGames,
            IStoredSqlFunctionCaller sqlFunctionCaller,
            ITeamnameDTOBuilder teamnameDTOBuilder,
            IImport_YoutubeDataRepository import_YoutubeDataRepository

            )
        {
            _appLogger = appLogger;
            _apiHelper = apiHelper;
            _Import_ScoreboardGamesRepository = import_ScoreboardGames;
            _sqlFunctionCaller = sqlFunctionCaller;
            _teamnameDTOBuilder = teamnameDTOBuilder;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
        }









        // Looks for team names either side of vs / versus, not case sensitive. 
        public List<string?> ExtractTeamNamesAroundVsKeyword(string youtubeTitle)
        {
            var match = TeamNamePattern.Match(youtubeTitle);

            List<string?> teams = new List<string?>();

            if (match.Success)
            {
                string team1 = match.Groups[1].Value;
                string team2 = match.Groups[2].Value;

                teams.Add(team1);
                teams.Add(team2);

                return teams;
            }
            else
            {
                _appLogger.Info($"No matches found for {nameof(ExtractTeamNamesAroundVsKeyword)}, ");
                Console.WriteLine($"No matches found for {youtubeTitle}.");
                return teams;
            }
        }






        public async Task<List<string>> ExtractEndTeamStringForMultipleAsync(List<Import_YoutubeDataEntity> youtubeVideos)
        {
            List<(string, string)> missingSeperatorResults = new List<(string, string)>();

            List<string> ExtractedEndStrings = new List<string>();

            if (youtubeVideos.Count == 0)
            {
                _appLogger.Error($"No YoutubeEntities found for parameter in {nameof(ExtractEndTeamStringForMultipleAsync)}.");
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




        public async Task<string> ExtractEndTeamStringAsync(Import_YoutubeDataEntity youtubeVideo)
        {
            if (youtubeVideo == null)
            {
                _appLogger.Error($"No YoutubeEntity provided for parameter in {nameof(ExtractEndTeamStringAsync)}.");
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


    }
}
