using System.ComponentModel.DataAnnotations;

namespace BookPortal.Web.Models
{
    public class AwardRequest
    {
        public int? Limit { get; set; } = 25;

        public int? Offset { get; set; }

        public AwardSort Sort { get; set; } = AwardSort.Rusname;

        public bool IsOpened { get; set; }
    }

    public enum AwardSort
    {
        Id = 1,
        Rusname = 2,
        Language = 3,
        Country = 4
    }
}
