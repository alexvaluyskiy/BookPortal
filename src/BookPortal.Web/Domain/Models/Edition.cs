namespace BookPortal.Web.Domain.Models
{
    public class Edition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Authors { get; set; }

        public string Publishers { get; set; }

        public string Series { get; set; }

        public int Count { get; set; }

        public string Format { get; set; }

        public int Pages { get; set; }

        public int Year { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}
