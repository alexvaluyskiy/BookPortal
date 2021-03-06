﻿using System.Collections.Generic;

namespace BookPortal.Web.Models.Responses
{
    public class SerieResponse
    {
        public int SerieId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? YearOpen { get; set; }

        public int? YearClose { get; set; }

        public bool SerieClosed { get; set; }

        public int? LanguageId { get; set; }

        public string LanguageName { get; set; }

        public List<PublisherResponse> Publishers { get; set; } 
    }
}
