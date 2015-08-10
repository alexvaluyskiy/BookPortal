﻿namespace BookPortal.Web.Models.Responses
{
    public class MarkResponse
    {
        public int WorkId { get; set; }

        public double Rating { get; set; }

        public int MarksCount { get; set; }

        public int? UserMark { get; set; }
    }
}
