using System.ComponentModel.DataAnnotations;

namespace BookPortal.Web.Models
{
    public class TranslationRequest
    {
        [Required]
        public int PersonId { get; set; }

        public TranslatorSort Sort { get; set; } = TranslatorSort.Type;
    }
}
