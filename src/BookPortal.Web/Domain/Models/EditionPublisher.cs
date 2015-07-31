namespace BookPortal.Web.Domain.Models
{
    public class EditionPublisher
    {
        public int Id { get; set; }

        public int EditionId { get; set; }
        public Edition Edition { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
