using System.Collections.Generic;

namespace BookPortal.Web.Models.Responses
{
    public class WorkResponse
    {
        public int WorkId { get; set; }

        public List<PersonResponse> Persons { get; set; } 

        public string RusName { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public int? Year { get; set; }

        public string Description { get; set; }

        public int WorkTypeId { get; set; }

        public string WorkTypeName { get; set; }

        public int WorkTypeLevel { get; set; }

        public List<int> ChildWorks { get; set; }

        public object GroupIndex { get; set; }

        public bool IsPlan { get; set; }

        public bool NotFinished { get; set; }

        public bool Published { get; set; }

        // public int? ParentWorkId { get; set; }
    }
}
