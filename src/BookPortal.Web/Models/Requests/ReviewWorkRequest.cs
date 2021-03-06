﻿using System.ComponentModel.DataAnnotations;
using BookPortal.Web.Models.Types;

namespace BookPortal.Web.Models.Requests
{
    public class ReviewWorkRequest
    {
        [Required]
        public int WorkId { get; set; }

        public int Limit { get; set; } = 15;

        public int Offset { get; set; }

        public ReviewSort Sort { get; set; } = ReviewSort.Date;
    }
}
