﻿// Ignore Spelling: Teamnames


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


        // Separate teams with the same name will have the nationality identified in the inputs within brackets 
        // E.g  Cerberus eSports, Inputs: {"cerberus esports (filipino team);cerberus esports ph;crbl ladies;cerberus ph;cerberus esports philippines;cer;cerberus esports (philippines)"}

        public List<string>? Inputs { get; set; }
    }
}
