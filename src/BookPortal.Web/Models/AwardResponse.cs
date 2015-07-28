using System;

namespace BookPortal.Web.Models
{
    public class AwardResponse
    {
        public int AwardId { get; set; }

        public string Name { get; set; }

        public string RusName { get; set; }

        public string Homepage { get; set; }

        public string Description { get; set; }

        public string DescriptionSource { get; set; }

        public string Notes { get; set; }

        public bool AwardClosed { get; set; }

        public bool IsOpened { get; set; }

        public int? LanguageId { get; set; }

        public string LanguageName { get; set; }

        public int? CountryId { get; set; }

        public string CountryName { get; set; }

        public string FirstContestDate { get; set; }

        public string LastContestDate { get; set; }
    }
}
