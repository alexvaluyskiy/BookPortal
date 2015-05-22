using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Web.Models
{
    public class SerieRequest
    {
        public int SerieId { get; set; }

        public int Limit { get; set; } = 25;

        public int Offset { get; set; }

        public SerieEditionsSort Sort { get; set; } = SerieEditionsSort.Order;
    }
}
