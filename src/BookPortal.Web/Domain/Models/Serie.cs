using System;

namespace BookPortal.Web.Domain.Models
{
    public class Serie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateOpen { get; set; }

        public DateTime? DateClose { get; set; }

        public bool SerieClosed { get; set; }

        public int? ParentSerieId { get; set; }
        public Serie ParentSerie { get; set; }

        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
