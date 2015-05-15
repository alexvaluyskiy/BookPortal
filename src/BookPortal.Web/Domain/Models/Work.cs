namespace BookPortal.Web.Domain.Models
{
    public class Work
    {
        public int Id { get; set; }

        public string RusName { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }
    }
}
