using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Reviews.Model
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

        public DateTime DateCreated { get; set; }
    }
}
