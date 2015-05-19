namespace BookPortal.Reviews.Model
{
    public class ReviewRequest
    {
        public int? Limit { get; set; } = 25;

        public int? Offset { get; set; }

        public ReviewSort Sort { get; set; } = ReviewSort.Date;

        public int WorkId { get; set; }

        public int PersonId { get; set; }

        public int UserId { get; set; }
    }
}
