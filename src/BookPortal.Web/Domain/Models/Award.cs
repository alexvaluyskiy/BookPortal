using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BookPortal.Web.Domain.Models
{
    [Table("awards")]
    public class Award
    {
        [Key]
        [Column("award_id")]
        public int Id { get; set; }

        [Column("rusname")]
        public string RusName { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("homepage")]
        public string Homepage { get; set; }

        [Column("description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Column("description_copyright")]
        public string DescriptionCopyright { get; set; }

        [Column("notes")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Column("award_close")]
        public bool AwardClosed { get; set; }

        [Column("is_opened")]
        public bool IsOpened { get; set; }

        [Column("language_id")]
        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }

        [Column("country_id")]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contest> Contests { get; set; } = new HashSet<Contest>();

        [JsonIgnore]
        public virtual ICollection<Nomination> Nominations { get; set; } = new HashSet<Nomination>();
    }
}
