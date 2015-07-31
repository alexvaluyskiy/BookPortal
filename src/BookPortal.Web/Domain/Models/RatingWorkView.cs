using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("rating_work_view")]
    public class RatingWorkView
    {
        [Column("rating_work_id")]
        public int Id { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }
        public Work Work { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("marks_count")]
        public int MarksCount { get; set; }

        [Column("rating_type")]
        [MaxLength(50)]
        public string RatingType { get; set; }
    }
}
