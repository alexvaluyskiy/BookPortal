using System;

namespace BookPortal.Responses.Domain.Models
{
    public class Response
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int WorkId { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
