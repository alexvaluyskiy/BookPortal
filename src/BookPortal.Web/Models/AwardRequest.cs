namespace BookPortal.Web.Models
{
    public class AwardRequest
    {
        public int? Limit { get; set; } = 25;

        public int? Offset { get; set; }

        public bool IncludeContests { get; set; }

        public bool IncludeNominations { get; set; }

        public bool IncludeContestsWorks { get; set; }
    }
}
