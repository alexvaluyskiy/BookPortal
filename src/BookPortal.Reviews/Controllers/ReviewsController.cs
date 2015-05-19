using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Reviews.Domain.Models;
using BookPortal.Reviews.Model;
using BookPortal.Reviews.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Reviews.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ReviewsService _reviewsService;

        public ReviewsController(ReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet("api/persons/{personId}/reviews")]
        public async Task<IActionResult> IndexPerson(ReviewPersonRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsPersonAsync(reviewRequest);

            return new WrappedObjectResult(reviews);
        }

        [HttpGet("api/works/{workId}/reviews")]
        public async Task<IActionResult> IndexWork(ReviewWorkRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsWorkAsync(reviewRequest);

            return new WrappedObjectResult(reviews);
        }

        [HttpGet("api/users/{userId}/reviews")]
        public async Task<IActionResult> IndexUser(ReviewUserRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsUserAsync(reviewRequest);

            return new WrappedObjectResult(reviews);
        }

        [HttpGet("api/[controller]/{reviewId}", Name = "GetReview")]
        public async Task<IActionResult> Get(int reviewId)
        {
            var review = await _reviewsService.GetReviewAsync(reviewId);

            if (review == null)
                return new WrappedErrorResult(404);

            return new WrappedObjectResult(review);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Review request)
        {
            ReviewResponse review = await _reviewsService.AddReviewAsync(request);

            if (review == null)
                return new WrappedErrorResult(400);

            return new CreatedAtRouteResult("GetReview", new { reviewId = review.Id }, review);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> Put(int reviewId, [FromBody]Review request)
        {
            ReviewResponse review = await _reviewsService.UpdateReviewAsync(request);

            if (review == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            ReviewResponse review = await _reviewsService.DeleteReviewAsync(reviewId);

            if (review == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }
    }
}
