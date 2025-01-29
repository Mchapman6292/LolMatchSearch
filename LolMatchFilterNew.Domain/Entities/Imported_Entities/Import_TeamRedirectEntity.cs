using System.ComponentModel.DataAnnotations;

namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities
{
    public class Import_TeamRedirectEntity
    {

        // Composite key of both fields since multiple AllNames can share the same PageName
        [Key]
        public string PageName { get; set; }

        [Key]
        public string? AllName { get; set; }

        public string? OtherName { get; set; }
        public string? UniqueLine { get; set; }
    }
}
