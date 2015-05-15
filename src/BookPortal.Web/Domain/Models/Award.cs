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

        public string DescriptionSource { get; set; }

        public string Notes { get; set; }

        public bool AwardClosed { get; set; }

        public bool IsOpened { get; set; }

        public int? LanguageId { get; set; }
        public Language Language { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Contest> Contests { get; set; } = new HashSet<Contest>();
        public ICollection<Nomination> Nominations { get; set; } = new HashSet<Nomination>();
    }
}
