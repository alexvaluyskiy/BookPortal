using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Ratings.Domain.Models
{
    [Table("review_votes")]
    public class ReviewVote
    {
        [Column("reviews_vote_id")]
        public int Id { get; set; }

        [Column("review_id")]
        public int ReviewId { get; set; }
        public Review Review { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("vote")]
        public int Vote { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }
    }
}
