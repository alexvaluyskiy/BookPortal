using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("countries")]
    public class Country
    {
        [Column("country_id")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}