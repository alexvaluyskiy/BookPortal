using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("languages")]
    public class Language
    {
        [Column("language_id")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}