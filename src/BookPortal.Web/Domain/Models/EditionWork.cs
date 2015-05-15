namespace BookPortal.Web.Domain.Models
{
    public class EditionWork
    {
        public int Id { get; set; }

        public int EditionId { get; set; }
        public Edition Edition { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}
