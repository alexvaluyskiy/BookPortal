using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("contests_works")]
    public class ContestWork
    {
        [Key]
        [Column("contest_work_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("rusname")]
        public string RusName { get; set; }

        [Column("prefix")]
        public string Prefix { get; set; }

        [Column("postfix")]
        public string Postfix { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("is_winner")]
        public bool IsWinner { get; set; }

        //public ContestWorkType LinkType { get; set; }
        //public int LinkID { get; set; }

        [Column("contest_id")]
        public int ContestId { get; set; }
        public virtual Contest Contest { get; set; }

        [Column("nomination_id")]
        public int NominationId { get; set; }
        public virtual Nomination Nomination { get; set; }
    }
}