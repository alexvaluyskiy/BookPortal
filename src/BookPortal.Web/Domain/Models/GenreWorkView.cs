using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("genre_works_view")]
    public class GenreWorkView
    {
        [Column("genre_work_view_id")]
        public int Id { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }
        public Work Work { get; set; }

        [Column("genre_work_id")]
        public int GenreWorkId { get; set; }

        [Column("genre_count")]
        public int GenreCount { get; set; }
    }
}
