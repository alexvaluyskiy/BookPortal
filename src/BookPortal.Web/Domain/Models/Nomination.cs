using System.Collections.Generic;

namespace BookPortal.Web.Domain.Models
{
    public class Nomination
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RusName { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public int AwardId { get; set; }

        public Award Award { get; set; }

        public virtual ICollection<ContestWork> ContestWorks { get; set; } = new HashSet<ContestWork>();
    }
}
