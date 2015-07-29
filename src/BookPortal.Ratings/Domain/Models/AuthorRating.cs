using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Ratings.Domain.Models
{
    [Table("autor_rating")]
    public class AuthorRating
    {
        [Column("author_rating_id")]
        public int AuthorRatingId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

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
