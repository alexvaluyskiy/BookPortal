using System;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Models
{
    public class PublisherResponse
    {
        public int PublisherId { get; set; }

        public string Name { get; set; }

        public PublisherType Type { get; set; }

        public DateTime? DateOpen { get; set; }

        public DateTime? DateClose { get; set; }

        public string Description { get; set; }

        public string DescriptionSource { get; set; }

        public int? LanguageId { get; set; }

        public string LanguageName { get; set; }
    }
}
