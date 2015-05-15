using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Web.Models
{
    public class TranslationResponse
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int WorkId { get; set; }
        public string WorkName { get; set; }
        public int WorkYear { get; set; }
        public int TranslationYear { get; set; }
        public string WorkTypeName { get; set; }
        public string WorkTypeSingle { get; set; }
        public int WorkTypeLevel { get; set; }
        public List<string> Names { get; set; }
    }
}
