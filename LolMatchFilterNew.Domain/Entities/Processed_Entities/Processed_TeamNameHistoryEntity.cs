
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities
{
    public class Processed_TeamNameHistoryEntity
    {
        // NEED TO DISTINGUISH BETWEEN ACQUISITIONS/SISTER TEAMS & RENAMES

        [Key]
        public string CurrentTeamName { get; set; }

        // For single renames
        public List<string> NameHistory { get; set; }

    }
}
