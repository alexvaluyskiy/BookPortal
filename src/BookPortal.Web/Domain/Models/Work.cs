using System.Collections.Generic;

namespace BookPortal.Web.Domain.Models
{
    public class Work
    {
        public int Id { get; set; }

        public string RusName { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }

        public ICollection<PersonWork> Persons { get; set; }
    }
}
