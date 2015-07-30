namespace BookPortal.Ratings.Models
{
    public class AuthorRatingResponse
    {
        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public string PersonNameOriginal { get; set; }

        public double Rating { get; set; }

        public int MarksWeight { get; set; }

        public int MarksCount { get; set; }

        public int UsersCount { get; set; }
        
    }
}
