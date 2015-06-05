using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Web.Models
{
    public class WorkResponse
    {
        public int WorkId { get; set; }

        public string RusName { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public int WorkTypeId { get; set; }

        public string WorkTypeName { get; set; }

        public int WorkTypeLevel { get; set; }

        public List<WorkResponse> ChildWorks { get; set; } = new List<WorkResponse>(); 
    }
}
