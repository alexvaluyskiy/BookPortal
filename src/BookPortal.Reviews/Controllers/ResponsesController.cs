using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Reviews.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Reviews.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        private readonly ReviewsService _reviewsService;

        public ReviewsController(ReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var persons = await _reviewsService.GetResponsesAsync();

            return new WrappedObjectResult(persons);
        }

        [HttpGet("{responseid}")]
        public async Task<IActionResult> Get(int responseId)
        {
            var person = await _reviewsService.GetResponseAsync(responseId);

            if (person == null)
                return new WrappedErrorResult(404);

            return new WrappedObjectResult(person);
        }
    }
}
