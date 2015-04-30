using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("languages")]
    public class Language
    {
        [Key]
        [Column("language_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }
    }
}