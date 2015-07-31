namespace BookPortal.Web.Models.Requests
{
    public class SerieRequest
    {
        public int SerieId { get; set; }

        public int Limit { get; set; } = 25;

        public int Offset { get; set; }

        public EditionsSort Sort { get; set; } = EditionsSort.Order;
    }
}
