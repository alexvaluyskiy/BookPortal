namespace BookPortal.Web.Domain.Models
{
    public class EditionTranslation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EditionId { get; set; }
        public Edition Edition { get; set; }

        public int TranslationWorkId { get; set; }
        public TranslationWork TranslationWork { get; set; }
    }
}