using System.ComponentModel.DataAnnotations;

namespace BookPortal.Reviews.Model
{
    public class ReviewUserRequest
    {
        [Required]
        public int UserId { get; set; }

        public int? Limit { get; set; } = 25;

        public int? Offset { get; set; }

        public ReviewSort Sort { get; set; } = ReviewSort.Date;
    }
}
