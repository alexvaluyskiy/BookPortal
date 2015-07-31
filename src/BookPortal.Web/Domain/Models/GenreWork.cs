using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("genre_works")]
    public class GenreWork
    {
        [Column("genre_work_id")]
        public int Id { get; set; }

        [Column("parent_genre_work_id")]
        public int ParentGenreWorkId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("genre_work_group_id")]
        public int GenreWorkGroupId { get; set; }

        [Column("level")]
        public int Level { get; set; }
    }
}
