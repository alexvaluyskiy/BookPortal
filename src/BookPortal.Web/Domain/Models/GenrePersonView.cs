using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("genre_persons_view")]
    public class GenrePersonView
    {
        [Column("genre_person_view_id")]
        public int Id { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [Column("genre_work_id")]
        public int GenreWorkId { get; set; }
        public GenreWorkUser GenreWork { get; set; }

        [Column("genre_count")]
        public int GenreCount { get; set; }

        [Column("genre_total")]
        public int GenreTotal { get; set; }
    }
}
