using System.ComponentModel.DataAnnotations;

namespace BookPortal.Reviews.Model
{
    public class ReviewPersonRequest
    {
        [Required]
        public int PersonId { get; set; }

        public int? Limit { get; set; } = 25;

        public int? Offset { get; set; }

        public ReviewSort Sort { get; set; } = ReviewSort.Date;
    }
}
