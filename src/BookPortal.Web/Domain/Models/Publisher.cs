using System;
using System.Collections.Generic;

namespace BookPortal.Web.Domain.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PublisherType Type { get; set; } = PublisherType.Paper;

        public DateTime? DateOpen { get; set; }

        public DateTime? DateClose { get; set; }

        public string Description { get; set; }

        public string DescriptionSource { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<PublisherSerie> Series { get; set; } = new HashSet<PublisherSerie>();
    }
}
