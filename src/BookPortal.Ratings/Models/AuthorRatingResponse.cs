using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Ratings.Models
{
    public class AuthorRatingResponse
    {
        public int PersonId { get; set; }
        public double Rating { get; set; }
        public int MarksWeight { get; set; }
        public int MarksCount { get; set; }
        public int UsersCount { get; set; }
    }
}
