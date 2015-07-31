using System.ComponentModel.DataAnnotations;
using BookPortal.Web.Models.Types;

namespace BookPortal.Web.Models.Requests
{
    public class ReviewPersonRequest
    {
        [Required]
        public int PersonId { get; set; }

        public int Limit { get; set; } = 25;

        public int Offset { get; set; }

        public ReviewSort Sort { get; set; } = ReviewSort.Date;
    }
}
