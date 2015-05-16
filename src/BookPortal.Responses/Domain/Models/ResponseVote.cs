namespace BookPortal.Responses.Domain.Models
{
    public class ResponseVote
    {
        public int Id { get; set; }

        public int ResponseId { get; set; }
        public Response Response { get; set; }

        public int UserId { get; set; }

        public int Vote { get; set; }
    }
}
