using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Activity = System.Diagnostics.Activity;

namespace LolMatchFilterNew.Domain.Interfaces.IYoutubeApi
{
    public interface IYoutubeApi
    {
        Task GetAndDocumentVideoDataAsync(Activity activity, string videoTitle, string gameId, List<string> teamNames);

        Task<List<PlaylistItem>> GetYoutubeVideoPlaylistAsync(string videoId);
    }
}
