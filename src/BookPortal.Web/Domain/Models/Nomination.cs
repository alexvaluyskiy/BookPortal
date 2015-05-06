using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("nominations")]
    public class Nomination
    {
        [Key]
        [Column("nomination_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("rusname")]
        public string RusName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("award_id")]
        public int AwardId { get; set; }
        public Award Award { get; set; }

        public virtual ICollection<ContestWork> ContestWorks { get; set; } = new HashSet<ContestWork>();
    }
}
