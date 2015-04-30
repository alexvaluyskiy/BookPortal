using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("contests")]
    public class Contest
    {
        [Key]
        [Column("contest_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        //по этому значению идет сортировка (обычно тоже значение, что и в Name)
        [Column("nameyear")]
        public int NameYear { get; set; }

        //доп. сортировка для конкурсов
        [Column("number")]
        public int Number { get; set; }

        [Column("place")]
        public string Place { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Column("award_id")]
        public int AwardId { get; set; }
        public virtual Award Award { get; set; }

        public virtual ICollection<ContestWork> ContestWorks { get; set; } = new HashSet<ContestWork>();
    }
}