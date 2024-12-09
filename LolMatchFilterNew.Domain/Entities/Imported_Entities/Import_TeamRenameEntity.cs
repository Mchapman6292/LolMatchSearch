using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
    public class Import_TeamRenameEntity
    {
        [Key]
        [Required]

        public string OriginalName { get; set; }

        [Key]
        [Required]
        public string NewName { get; set; }

        [Key]
        [Required]
        public DateTime Date { get; set; }


        public string? Verb { get; set; }


        public string? IsSamePage { get; set; }

        public string? NewsId { get; set; }
    }

