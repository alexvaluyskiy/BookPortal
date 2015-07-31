using System;

namespace BookPortal.Web.Models.Responses
{
    public class ReviewResponse
    {
        public int ReviewId { get; set; }

        public string Text { get; set; }

        public int WorkId { get; set; }

        public string WorkRusName { get; set; }

        public string WorkName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int UserWorkRating { get; set; }

        public int ReviewRating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
