using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs;
using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchSearches
{
    public interface IMatchSearch
    {
        Task<List<string>> ExtractEndTeamStringForMultiple(List<Import_YoutubeDataEntity> youtubeVideos);
        Task<string> ExtractEndTeamString(Import_YoutubeDataEntity youtubeVideo);
    }
}
