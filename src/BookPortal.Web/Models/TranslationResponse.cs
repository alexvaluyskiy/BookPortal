using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Models
{
    public class TranslationResponse
    {
        public int Id { get; set; }

        public List<Person> Translators { get; set; }

        public int WorkId { get; set; }

        public string WorkName { get; set; }

        public int WorkYear { get; set; }

        public List<Person> Authors { get; set; }

        public int TranslationYear { get; set; }

        public string WorkTypeName { get; set; }

        public string WorkTypeNameSingle { get; set; }

        public int WorkTypeLevel { get; set; }

        public List<string> Names { get; set; }

        public List<int> Editions { get; set; }
    }
}
