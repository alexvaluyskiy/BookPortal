namespace BookPortal.Web.Models.Responses
{
    public class PersonResponse
    {
        public int PersonId { get; set; }

        public string Name { get; set; }

        public string NameRp { get; set; }

        public string NameOriginal { get; set; }

        public string NameSort { get; set; }

        public int? Gender { get; set; }

        public string Birthdate { get; set; }

        public string Deathdate { get; set; }

        public int? CountryId { get; set; }

        public string CountryName { get; set; }

        public int? LanguageId { get; set; }

        public string LanguageName { get; set; }

        public string Biography { get; set; }

        public string BiographySource { get; set; }

        public string Notes { get; set; }
    }
}
