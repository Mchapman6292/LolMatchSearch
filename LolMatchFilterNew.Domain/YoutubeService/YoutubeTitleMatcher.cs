using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Google.Apis.YouTube.v3;
using Activity = System.Diagnostics.Activity;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoData;
using LolMatchFilterNew.Domain.Entities;
using System.Text.RegularExpressions;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace LolMatchFilterNew.Domain.YoutubeVideoInfo.YoutubeTitleMatcher
{
    public class YoutubeTitleMatcher : IYoutubeTitleMatcher
    {
        private readonly IAppLogger _appLogger;




        public YoutubeTitleMatcher(IAppLogger appLogger)
        {
            _appLogger = appLogger;

        }






        public async Task<List<string>> ExtractTeamNames(Activity activity, string youtubeTitle)
        {
            _appLogger.Info($"Starting {nameof(ExtractTeamNames)} TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");

            try
            {

                if (await CheckYoutubeTitleFormatForAbbreviatedTeams(activity, youtubeTitle) == false)
                {
                    _appLogger.Error($"youtubeTitle: {youtubeTitle} does not match expected format,empty list returned. TraceId:{activity.TraceId}.");
                    return new List<string>();
                }

                var match = Regex.Match(youtubeTitle, @"^([A-Z0-9]{2,3})\s+vs\s+([A-Z0-9]{2,3})", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var teamNames = new List<string> { match.Groups[1].Value, match.Groups[2].Value };
                    _appLogger.Info($"Successfully extracted team names: {string.Join(", ", teamNames)}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                    return teamNames;

                }
                else
                {
                    _appLogger.Warning($"Failed to extract team names from valid format. Title: '{youtubeTitle}', TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in {nameof(ExtractTeamNames)}: {ex.Message}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}", ex);
                return new List<string>();
            }
        }




        // Example format: "G2 vs FNC" or "T1 vs SKT"
        public async Task<bool> CheckYoutubeTitleFormatForAbbreviatedTeams(Activity activity, string youtubeTitle)
        {
            _appLogger.Info($"Starting {nameof(CheckYoutubeTitleFormatForAbbreviatedTeams)}, TraceId: {activity.TraceId}.");
            try
            {
                string pattern = @"^([A-Z0-9]{2,3})\s+vs\s+([A-Z0-9]{2,3})";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                bool isMatch = regex.IsMatch(youtubeTitle);

                _appLogger.Info($"YouTube title format check result: {isMatch}. Title: '{youtubeTitle}', TraceId: {activity.TraceId}.");

                return isMatch;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in {nameof(CheckYoutubeTitleFormatForAbbreviatedTeams)}: {ex.Message}. TraceId: {activity.TraceId}", ex);
                return false;
            }
        }
    }
}
