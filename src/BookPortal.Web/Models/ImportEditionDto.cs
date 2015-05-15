using System;
using BookPortal.Web.Domain.Models.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BookPortal.Web.Models
{
    public class ImportEditionDto
    {
        public string Isbn { get; set; }

        public string Name { get; set; }

        public string Authors { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EditionType Type { get; set; }

        public string Publishers { get; set; }

        public int? Year { get; set; }

        public int Pages { get; set; }

        public string Language { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EditionCoverType CoverType { get; set; }

        public bool SuperCover { get; set; }

        public string Format { get; set; }

        public int Count { get; set; }

        public string Serie { get; set; }

        public string Annotation { get; set; }

        public Uri CoverUri { get; set; }
    }
}