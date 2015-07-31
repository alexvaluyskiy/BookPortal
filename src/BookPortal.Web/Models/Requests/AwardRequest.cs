namespace BookPortal.Web.Models.Requests
{
    public class AwardRequest
    {
        public int Limit { get; set; } = 25;

        public int Offset { get; set; } = 0;

        public AwardSort Sort { get; set; } = AwardSort.Id;

        public bool IsOpened { get; set; }
    }
}
