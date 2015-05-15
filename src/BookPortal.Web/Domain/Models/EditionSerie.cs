namespace BookPortal.Web.Domain.Models
{
    public class EditionSerie
    {
        public int Id { get; set; }

        public int EditionId { get; set; }
        public Edition Edition { get; set; }

        public int SerieId { get; set; }
        public Serie Serie { get; set; }
    }
}
