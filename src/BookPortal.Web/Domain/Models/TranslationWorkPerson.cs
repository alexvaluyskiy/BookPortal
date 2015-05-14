namespace BookPortal.Web.Domain.Models
{
    public class TranslationWorkPerson
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int TranslationWorkId { get; set; }
        public TranslationWork TranslationWork { get; set; }
    }
}