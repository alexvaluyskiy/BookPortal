using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Ratings.Models
{
    public class WorkRatingResponse
    {
        public int WorkId { get; set; }
        public double Rating { get; set; }
        public int MarkCount { get; set; }
        public int WorkType { get; set; }
    }
}
