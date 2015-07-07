namespace BookPortal.Web.Domain.Models
{
    public class PublisherSerie
    {
        public int Id { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public int SerieId { get; set; }
        public Serie Serie { get; set; }
    }
}
