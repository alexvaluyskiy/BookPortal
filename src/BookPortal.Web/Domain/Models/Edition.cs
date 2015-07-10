using System;
using System.Collections.Generic;
using BookPortal.Web.Domain.Models.Types;

namespace BookPortal.Web.Domain.Models
{
    public class Edition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EditionType Type { get; set; } = EditionType.Normal;

        public string Authors { get; set; }

        public string Compilers { get; set; }

        public string Isbn { get; set; }

        public int Year { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? Count { get; set; }

        public EditionCoverType CoverType { get; set; } = EditionCoverType.Unknown;

        public bool SuperCover { get; set; }

        public string Format { get; set; }

        public int? Pages { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Notes { get; set; }

        public int? LanguageId { get; set; }
        public Language Language { get; set; }

        public ICollection<EditionWork> Works { get; set; } = new HashSet<EditionWork>();

        public ICollection<EditionSerie> Series { get; set; } = new HashSet<EditionSerie>();

        public ICollection<EditionPublisher> Publishers { get; set; } = new HashSet<EditionPublisher>();
    }
}
