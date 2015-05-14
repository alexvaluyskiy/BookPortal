using System.Collections.Generic;

namespace BookPortal.Web.Domain.Models
{
    public class Award
    {
        public int Id { get; set; }

        public string RusName { get; set; }

        public string Name { get; set; }

        public string Homepage { get; set; }

        public string Description { get; set; }

        public string DescriptionCopyright { get; set; }

        public string Notes { get; set; }

        public bool AwardClosed { get; set; }

        public bool IsOpened { get; set; }

        public int? LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Contest> Contests { get; set; } = new HashSet<Contest>();

        public virtual ICollection<Nomination> Nominations { get; set; } = new HashSet<Nomination>();
    }
}
