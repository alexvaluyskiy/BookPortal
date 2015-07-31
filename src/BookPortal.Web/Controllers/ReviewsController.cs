using System.Threading.Tasks;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        private readonly ReviewsService _reviewsService;

        public ReviewsController(ReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            var review = await _reviewsService.GetReviewAsync(reviewId);

            if (review == null)
                return this.ErrorObject(404, $"Review (id: {reviewId}) is not found");

            return this.SingleObject(review);
        }
    }
}
