using System.Collections;
using System.Collections.Generic;
using BookPortal.Ratings.Models.Shims;

namespace BookPortal.Ratings.Models
{
    public class WorkRatingResponse
    {
        public int WorkId { get; set; }

        public string WorkRusName { get; set; }

        public string WorkName { get; set; }

        public int WorkYear { get; set; }

        public IEnumerable<PersonResponseShim> Persons { get; set; }

        public double Rating { get; set; }

        public int MarksCount { get; set; }
    }
}
