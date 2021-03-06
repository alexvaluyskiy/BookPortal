﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortal.Web.Domain.Models
{
    [Table("marks")]
    public class Mark
    {
        [Column("mark_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }

        [Column("mark_value")]
        public int MarkValue { get; set; }
    }
}
