using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Ratings.Domain.Models
{
    [Table("work_rating")]
    public class WorkRating
    {
        [Column("work_rating_id")]
        public int WorkRatingId { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("marks_count")]
        public int MarksCount { get; set; }

        [Column("rating_type")]
        public string RatingType { get; set; }
    }
}
