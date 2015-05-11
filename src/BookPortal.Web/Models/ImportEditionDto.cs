using System;
using System.Collections.Generic;
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
        public BookType BookType { get; set; }

        public string Publishers { get; set; }

        public int? Year { get; set; }

        public int Pages { get; set; }

        public string Language { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CoverType CoverType { get; set; }

        public bool SuperCover { get; set; }

        public string Format { get; set; }

        public int Count { get; set; }

        public string Serie { get; set; }

        public string Annotation { get; set; }

        public Uri CoverUri { get; set; }
    }

    public enum CoverType
    {
        Paperback = 1,
        Hardcover = 2
    }

    public enum BookType
    {
        Normal = 1,
        Collection = 2,
        Antology = 3
    }
}