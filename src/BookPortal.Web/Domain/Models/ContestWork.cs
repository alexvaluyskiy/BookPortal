using BookPortal.Web.Domain.Models.Types;

namespace BookPortal.Web.Domain.Models
{
    public class ContestWork
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RusName { get; set; }

        public string Prefix { get; set; }

        public string Postfix { get; set; }

        public int Number { get; set; }

        public bool IsWinner { get; set; }

        public ContestWorkType LinkType { get; set; }

        public int? LinkId { get; set; }

        public int ContestId { get; set; }
        public Contest Contest { get; set; }

        public int NominationId { get; set; }
        public Nomination Nomination { get; set; }
    }
}