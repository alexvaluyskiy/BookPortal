using System;

namespace BookPortal.Reviews.Domain.Models
{
    public class ReviewVote
    {
        public int Id { get; set; }

        public int ReviewId { get; set; }
        public Review Review { get; set; }

        public int UserId { get; set; }

        public int Vote { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
