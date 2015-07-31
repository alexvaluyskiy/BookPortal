using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("rating_work_expect_view")]
    public class RatingWorkExpectView
    {
        [Column("rating_work_expect_id")]
        public int Id { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }
        public Work Work { get; set; }

        [Column("plan_date")]
        public string PlanDate { get; set; }

        [Column("users_count")]
        public int UsersCount { get; set; }

        [Column("expect_type")]
        public int ExpectType { get; set; }
    }
}
