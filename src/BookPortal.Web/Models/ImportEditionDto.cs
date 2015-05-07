namespace BookPortal.Web.Models
{
    public class ImportEditionDto
    {
        public string Isbn { get; set; }

        public string Name { get; set; }

        public string Authors { get; set; }

        public string Publishers { get; set; }

        public string Year { get; set; }

        public string Pages { get; set; }

        public string Language { get; set; }

        public string CoverType { get; set; }

        public string Format { get; set; }

        public string Count { get; set; }

        public string Serie { get; internal set; }
    }
}