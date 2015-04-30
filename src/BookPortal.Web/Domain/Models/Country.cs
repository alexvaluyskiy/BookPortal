using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("countries")]
    public class Country
    {
        [Key]
        [Column("country_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }
    }
}