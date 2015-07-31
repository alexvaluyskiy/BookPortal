using System.Collections.Generic;

namespace BookPortal.Web.Models.Responses
{
    public class WorkRatingResponse
    {
        public int WorkId { get; set; }

        public string WorkRusName { get; set; }

        public string WorkName { get; set; }

        public int WorkYear { get; set; }

        public IEnumerable<PersonResponse> Persons { get; set; }

        public double Rating { get; set; }

        public int MarksCount { get; set; }
    }
}
