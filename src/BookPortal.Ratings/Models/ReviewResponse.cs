using System;

namespace BookPortal.Ratings.Models
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int WorkId { get; set; }

        public string WorkRusname { get; set; }

        public string WorkName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int UserWorkRating { get; set; }

        public int ReviewRating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
