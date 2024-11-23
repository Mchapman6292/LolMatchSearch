using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Processed_YoutubePlaylistEntities
{
    public class Processed_YoutubePlaylistEntity
    {
        [Key]
        public string name { get; set; }
        public string PlaylistId {  get; set; } 
    }
}
