using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController
{
    public interface IYoutubeController
    {
        Task FetchAndAddYoutubeVideo(List<string> playlistIds);
        Task FetchAndAddYoutubePlaylistsForChannel();
    }
}
