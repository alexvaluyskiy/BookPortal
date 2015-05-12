using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BookPortal.Web.Domain.Models
{
    [Table("contests_works")]
    public class ContestWork
    {
        [Key]
        [Column("contest_work_id")]
        public int Id { get; set; }

        // TODO: update if author or work was changed
        [Column("name")]
        public string Name { get; set; }

        // TODO: update if author or work was changed
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

        [Column("link_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContestWorkType? LinkType { get; set; }

        [Column("link_id")]
        public int? LinkId { get; set; }

        [Column("contest_id")]
        public int ContestId { get; set; }

        [JsonIgnore]
        public virtual Contest Contest { get; set; }

        [Column("nomination_id")]
        public int NominationId { get; set; }

        [JsonIgnore]
        public virtual Nomination Nomination { get; set; }
    }
}