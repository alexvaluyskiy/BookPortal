namespace BookPortal.Web.Models
{
    public class ContestWorkResponse
    {
        public int AwardId { get; set; }

        public string AwardRusname { get; set; }

        public string AwardName { get; set; }

        public bool AwardIsOpened { get; set; }

        public int ContestId { get; set; }

        public string ContestName { get; set; }
        
        public int ContestYear { get; set; }

        public int NominationId { get; set; }

        public string NominationRusname { get; set; }

        public string NominationName { get; set; }

        public int ContestWorkId { get; set; }

        public string ContestWorkRusname { get; set; }

        public string ContestWorkName { get; set; }

        public string ContestWorkPrefix { get; set; }

        public string ContestWorkPostfix { get; set; }
    }
}
