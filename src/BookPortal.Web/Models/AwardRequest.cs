namespace BookPortal.Web.Models
{
    public class AwardRequest
    {
        public int? Limit { get; set; } = 25;

        public int? Offset { get; set; }

        public AwardSort Sort { get; set; } = AwardSort.Rusname;

        public bool IsOpened { get; set; }
    }
}
