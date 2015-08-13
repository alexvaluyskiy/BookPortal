using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("work_types")]
    public class WorkType
    {
        [Column("work_type_id")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("name_single")]
        [MaxLength(50)]
        public string NameSingle { get; set; }

        [Column("level")]
        public int Level { get; set; }
    }
}
