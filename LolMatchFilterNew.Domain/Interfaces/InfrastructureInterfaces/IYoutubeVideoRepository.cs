using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository
{
    public interface IYoutubeVideoRepository
    {
        Task<int> BulkaddYoutubeDetails(IEnumerable<YoutubeVideoEntity> youtubeVideoDetails);
    }
}
