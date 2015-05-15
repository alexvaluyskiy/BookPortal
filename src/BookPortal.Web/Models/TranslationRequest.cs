using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Web.Models
{
    public class TranslationRequest
    {
        [Required]
        public int PersonId { get; set; }

        public TranslatorSort Sort { get; set; } = TranslatorSort.Type;
    }
}
