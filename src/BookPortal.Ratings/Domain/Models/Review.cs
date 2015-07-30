using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Ratings.Domain.Models
{
    [Table("reviews")]
    public class Review
    {
        [Column("review_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }
    }
}
