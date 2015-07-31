using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameRp { get; set; }

        public string NameOriginal { get; set; }

        public string NameSort { get; set; }

        public GenderType Gender { get; set; }

        public string Birthdate { get; set; }

        public string Deathdate { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        [Column("default_language_id")]
        public int? LanguageId { get; set; }
        public Language Language { get; set; }

        public string Biography { get; set; }

        public string BiographySource { get; set; }

        public string Notes { get; set; }

        [Column("is_opened")]
        public bool IsOpened { get; set; }
    }
}
