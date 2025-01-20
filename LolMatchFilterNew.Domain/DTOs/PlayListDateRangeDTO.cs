using LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PlayListDateRangeDTOs
{
    public class PlayListDateRangeDTO
    {
        public string PlaylistName {  get; set; }

        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        public List<Import_YoutubeDataEntity> Videos { get; set; }




    }
}
