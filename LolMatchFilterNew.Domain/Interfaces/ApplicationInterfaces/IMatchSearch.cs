using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.DTOs.YoutubeVideoResults;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchSearches
{
    public interface IMatchSearch
    {
        Task<List<string>> ExtractEndTeamStringFromYoutubeTitleList(List<YoutubeVideoEntity> youtubeVideos);
        Task<string> ExtractEndTeamStringFromYoutubeTitle(YoutubeVideoEntity youtubeVideo);
    }
}
