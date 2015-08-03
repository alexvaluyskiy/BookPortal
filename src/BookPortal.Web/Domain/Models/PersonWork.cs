using BookPortal.Web.Domain.Models.Types;

namespace BookPortal.Web.Domain.Models
{
    public class PersonWork
    {
        public int Id { get; set; }

        public WorkPersonType Type { get; set; }

        public int Order { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}