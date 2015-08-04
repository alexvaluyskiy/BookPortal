using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("genre_work_users")]
    public class GenreWorkUser
    {
        [Column("genre_work_user_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }
        public Work Work { get; set; }

        [Column("genre_work_id")]
        public int GenreWorkId { get; set; }
        public GenreWork GenreWork { get; set; }
    }
}
