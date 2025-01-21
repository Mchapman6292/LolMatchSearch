using Domain.DTOs.PlayListDateRangeDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IPlayListDateRangeDTOFactories
{
    public interface IPlayListDateRangeDTOFactory
    {
        List<PlayListDateRangeResult> CreateListOfPlaylistDateRangeDTOs(List<Import_YoutubeDataEntity> youtubeEntities);
    }
}
