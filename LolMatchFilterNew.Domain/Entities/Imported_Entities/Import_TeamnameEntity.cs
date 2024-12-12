// Ignore Spelling: Teamnames

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames
{
    public class Import_TeamnameEntity
    {
        [Key]

        public string TeamnameId { get; set; }

        public string Longname { get; set; }

        public string Short {  get; set; }  

        public string Medium { get; set; }

        public List<string> Inputs { get; set; }
    }
}
