using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Ratings.Models;
using BookPortal.Ratings.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Ratings.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ReviewsService _reviewsService;

        public ReviewsController(ReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet("api/reviews")]
        public async Task<IActionResult> Index(List<int> ids, int userId)
        {
            var review = await _reviewsService.GetReviewsAsync(ids, userId);

            if (review == null)
                return this.ErrorObject(404);

            return this.PageObject(review.Values, review.TotalRows);
        }

        [HttpGet("api/reviews/{reviewId}")]
        public async Task<IActionResult> Get(int reviewId)
        {
            var review = await _reviewsService.GetReviewAsync(reviewId);

            if (review == null)
                return this.ErrorObject(404);

            return this.SingleObject(review);
        }

        [HttpGet("api/persons/{personId}/reviews")]
        public async Task<IActionResult> IndexPerson(ReviewPersonRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsPersonAsync(reviewRequest);

            return this.PageObject(reviews);
        }

        [HttpGet("api/works/{workId}/reviews")]
        public async Task<IActionResult> IndexWork(ReviewWorkRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsWorkAsync(reviewRequest);

            return this.PageObject(reviews);
        }

        [HttpGet("api/users/{userId}/reviews")]
        public async Task<IActionResult> IndexUser(ReviewUserRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsUserAsync(reviewRequest);

            return this.PageObject(reviews);
        }
    }
}
