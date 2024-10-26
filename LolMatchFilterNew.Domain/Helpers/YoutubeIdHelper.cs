using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Helpers.YoutubeIdHelpers
{
    public static class YoutubeIdHelper
    {
        private const char QUOTE_CHAR = '\'';

        public static string WrapYoutubeIdInQuotationMarks(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                return string.Empty;

            return $"{QUOTE_CHAR}{videoId}";
        }


        public static string UnwrapYoutubeId(string quotedVideoId)
        {
            if (string.IsNullOrEmpty(quotedVideoId))
                return string.Empty;

            return quotedVideoId.TrimStart(QUOTE_CHAR);
        }

        public static bool IsProperlyQuoted(string videoId)
        {
            return !string.IsNullOrEmpty(videoId) &&
                        videoId.StartsWith(QUOTE_CHAR);
        }
    }

}

