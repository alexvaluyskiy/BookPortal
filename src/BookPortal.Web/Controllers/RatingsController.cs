using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class RatingsController : Controller
    {
        private readonly RatingsService _ratingsService;

        public RatingsController(RatingsService ratingsService)
        {
            _ratingsService = ratingsService;
        }

        [HttpGet("authors")]
        [Produces(typeof(IEnumerable<AuthorRatingResponse>))]
        public async Task<IActionResult> GetAuthorsRating()
        {
            var ratings = await _ratingsService.GetAuthorsRating();

            return this.PageObject(ratings.Values, ratings.TotalRows);
        }

        [HttpGet("works")]
        [Produces(typeof(IEnumerable<WorkRatingResponse>))]
        [ResponseCache(Duration = 3600)]
        public async Task<IActionResult> GetWorksRating(string type = "novelall")
        {
            var ratings = await _ratingsService.GetWorkRating(type);

            return this.PageObject(ratings.Values, ratings.TotalRows);
        }

        [HttpGet("expects")]
        [Produces(typeof(IEnumerable<RatingWorkExpectView>))]
        public async Task<IActionResult> GetWorkExpectRating(string type)
        {
            var ratings = await _ratingsService.GetWorkExpectRating(type);

            return this.PageObject(ratings.Values, ratings.TotalRows);
        }
    }
}
