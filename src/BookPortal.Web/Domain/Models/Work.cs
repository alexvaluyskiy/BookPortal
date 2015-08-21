using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookPortal.Web.Domain.Models.Types;

namespace BookPortal.Web.Domain.Models
{
    [Table("works")]
    public class Work
    {
        [Column("work_id")]
        public int Id { get; set; }

        [Column("rusname")]
        [MaxLength(255)]
        public string RusName { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("altname")]
        [MaxLength(255)]
        public string AltName { get; set; }

        [Column("year")]
        public int? Year { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("publish_type")]
        public int PublishType { get; set; }

        [Column("not_finished")]
        public bool NotFinished { get; set; }

        [Column("in_plans")]
        public bool InPlans { get; set; }

        [Column("show_in_biblio")]
        public byte ShowInBiblio { get; set; }

        [Column("show_subworks_in_biblio")]
        public byte ShowSubworksInBiblio { get; set; }

        [Column("work_type_id")]
        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }

        public ICollection<PersonWork> Persons { get; set; }
    }
}
