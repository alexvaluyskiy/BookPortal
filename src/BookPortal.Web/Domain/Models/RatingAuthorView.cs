using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("rating_author_view")]
    public class RatingAuthorView
    {
        [Column("rating_author_id")]
        public int Id { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("marks_weight")]
        public int MarksWeight { get; set; }

        [Column("marks_count")]
        public int MarksCount { get; set; }

        [Column("users_count")]
        public int UsersCount { get; set; }
    }
}
