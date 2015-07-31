namespace BookPortal.Web.Models.Responses
{
    public class ContestResponse
    {
        public int ContestId { get; set; }

        public string Name { get; set; }

        public int NameYear { get; set; }

        public int Number { get; set; }

        public string Place { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int AwardId { get; set; }
    }
}
