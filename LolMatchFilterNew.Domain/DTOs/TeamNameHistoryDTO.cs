using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TeamNameHistoryDTOs
{
    public class TeamNameHistoryDTO
    {
        public string PreviousName { get; set; }
        public string ChangedTo { get; set; }
        public string ChangeType { get; set; }
        public DateTime? ChangeDate { get; set; }

        // Records number of changes from the current name
        public int ChangeDepth { get; set; }

        //  Highlights the original team or organization that acquired or rebranded into the current name.
        public string ParentOrganization { get; set; }
    }
}
