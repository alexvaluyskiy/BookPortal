using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Domain.Models.Types;

namespace BookPortal.Web.Models
{
    public class EditionResponse
    {
        public int EditionId { get; set; }

        public string Name { get; set; }

        public EditionType? Type { get; set; }

        public string Authors { get; set; }

        public string Compilers { get; set; }

        public string Isbn { get; set; }

        public int? Year { get; set; }

        public string ReleaseDate { get; set; }

        public int? Count { get; set; }

        public EditionCoverType? CoverType { get; set; }

        public bool? SuperCover { get; set; }

        public string Format { get; set; }

        public int? Pages { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Notes { get; set; }

        public int? LanguageId { get; set; }

        public int? SerieSort { get; set; }

        public int Correct { get; set; }
    }
}
