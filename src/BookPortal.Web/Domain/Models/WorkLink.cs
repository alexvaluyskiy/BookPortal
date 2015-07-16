namespace BookPortal.Web.Domain.Models
{
    public class WorkLink
    {
        public int Id { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }

        public int? ParentWorkId { get; set; }
        public Work ParentWork { get; set; }

        public int LinkType { get; set; }

        public bool IsAddition { get; set; }

        public string BonusText { get; set; }

        public int? GroupIndex { get; set; }
    }
}
