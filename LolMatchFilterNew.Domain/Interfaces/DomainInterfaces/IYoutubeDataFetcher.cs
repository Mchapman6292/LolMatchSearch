using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher
{
    public interface IYoutubeDataFetcher
    {
        Task GetAllPlaylistsFromChannel(string channelId);

        Task GetFirstTwoPlaylistsFromChannel(string channelId);
    }
}
