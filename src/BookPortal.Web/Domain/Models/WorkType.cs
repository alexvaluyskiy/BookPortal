using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Web.Domain.Models
{
    public class WorkType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameSingle { get; set; }

        public int Level { get; set; }
    }
}
