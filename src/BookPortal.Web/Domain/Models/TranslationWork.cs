using System.Collections.Generic;

namespace BookPortal.Web.Domain.Models
{
    public class TranslationWork
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public ICollection<TranslationWorkPerson> TranslationWorkPersons { get; set; }
    }
}