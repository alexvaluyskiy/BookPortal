using System;
using System.Collections.Generic;

namespace BookPortal.Web.Models
{
    public class SerieResponse
    {
        public int SerieId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateOpen { get; set; }

        public DateTime? DateClose { get; set; }

        public bool SerieClosed { get; set; }

        public int? LanguageId { get; set; }

        public string LanguageName { get; set; }

        public List<PublisherResponse> Publishers { get; set; } 
    }
}
