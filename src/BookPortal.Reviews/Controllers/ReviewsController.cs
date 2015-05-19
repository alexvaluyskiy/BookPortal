using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Reviews.Model;
using BookPortal.Reviews.Services;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Reviews.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ReviewsService _reviewsService;
        private readonly TelemetryClient _client;

        public ReviewsController(ReviewsService reviewsService, TelemetryClient client)
        {
            _reviewsService = reviewsService;
            _client = client;
        }

        [HttpGet("api/works/{workId}/reviews")]
        public async Task<IActionResult> IndexWork(ReviewRequest reviewRequest)
        {
            _client.TrackEvent("get reviews by work");
            var persons = await _reviewsService.GetReviewsWorkAsync(reviewRequest);

            return new WrappedObjectResult(persons);
        }

        [HttpGet("api/[controller]/{reviewId}")]
        public async Task<IActionResult> Get(int reviewId)
        {
            var person = await _reviewsService.GetReviewAsync(reviewId);

            if (person == null)
                return new WrappedErrorResult(404);

            return new WrappedObjectResult(person);
        }
    }
}
