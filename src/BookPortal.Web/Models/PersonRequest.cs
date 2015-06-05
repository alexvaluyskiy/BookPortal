namespace BookPortal.Web.Models
{
    public class PersonRequest
    {
        public int Limit { get; set; } = 25;

        public int Offset { get; set; } = 0;

        public bool IsOpened { get; set; } = true;
    }
}