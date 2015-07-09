using System;
using System.Collections.Generic;

namespace BookPortal.Web.Domain.Models
{
    public class Serie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? YearOpen { get; set; }

        public int? YearClose { get; set; }

        public bool SerieClosed { get; set; }

        public int? ParentSerieId { get; set; }
        public Serie ParentSerie { get; set; }

        public int? LanguageId { get; set; }
        public Language Language { get; set; }

        public ICollection<PublisherSerie> Publishers { get; set; } = new HashSet<PublisherSerie>();
    }
}
