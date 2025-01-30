using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Imported_Entities.Import_LeagueEntities
{
    public class Import_LeagueEntity
    {
        [Key]
       public string LeagueName { get; set; }
       public string? LeagueShortName {  get; set; }
       public string? Region { get; set; } 
       public string? Level { get; set; }
       public string? IsOfficial { get; set; }

    }
}
