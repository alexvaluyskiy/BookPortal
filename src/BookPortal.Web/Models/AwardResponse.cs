using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Web.Models
{
    public class AwardResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RusName { get; set; }

        public string Homepage { get; set; }

        public string Description { get; set; }

        public string DescriptionCopyright { get; set; }

        public string Notes { get; set; }

        public bool AwardClosed { get; set; }

        public bool IsOpened { get; set; }

        public int? LanguageId { get; set; }

        public string LanguageName { get; set; }

        public int? CountryId { get; set; }

        public string CountryName { get; set; }

        public DateTime? FirstContestDate { get; set; }

        public DateTime? LastContestDate { get; set; }
    }
}
