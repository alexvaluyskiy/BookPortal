using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Ratings.Domain.Models
{
    [Table("work_expect_rating")]
    public class WorkExpectRating
    {
        [Column("work_expect_rating_id")]
        public int WorkExpectRatingId { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }

        [Column("plan_date")]
        public string PlanDate { get; set; }

        [Column("users_count")]
        public int UsersCount { get; set; }

        [Column("expect_type")]
        public int ExpectType { get; set; }
    }
}
