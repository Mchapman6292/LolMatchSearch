// Ignore Spelling: Teamnames


using System.ComponentModel.DataAnnotations;

namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames
{
    public class Import_TeamnameEntity
    {
        [Key]

        public string TeamnameId { get; set; }

        public string? Longname { get; set; }

        public string? Short {  get; set; }  

        public string? Medium { get; set; }

        public List<string>? Inputs { get; set; }
    }
}
