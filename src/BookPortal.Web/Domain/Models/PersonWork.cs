using System.ComponentModel.DataAnnotations.Schema;
using BookPortal.Web.Domain.Models.Types;

namespace BookPortal.Web.Domain.Models
{
    [Table("person_works")]
    public class PersonWork
    {
        [Column("person_work_id")]
        public int Id { get; set; }

        [Column("type")]
        public WorkPersonType Type { get; set; }

        [Column("order")]
        public int Order { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}