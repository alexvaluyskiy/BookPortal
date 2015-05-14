using System;
using System.Collections.Generic;

namespace BookPortal.Web.Domain.Models
{
    public class Contest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NameYear { get; set; }

        public int Number { get; set; }

        public string Place { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int AwardId { get; set; }
        public Award Award { get; set; }

        public ICollection<ContestWork> ContestWorks { get; set; } = new HashSet<ContestWork>();
    }
}