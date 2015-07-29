using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BookPortal.Ratings.Models;
using BookPortal.Ratings.Services;

namespace BookPortal.Ratings.Controllers
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
        [Produces(typeof(AuthorRatingResponse))]
        public async Task<IActionResult> GetAuthorsRating()
        {
            var ratings = await _ratingsService.GetAuthorsRating();

            return this.SingleObject(ratings);
        }

        [HttpGet("works")]
        [Produces(typeof(WorkRatingResponse))]
        public async Task<IActionResult> GetWorksRating(int type)
        {
            var ratings = await _ratingsService.GetWorkRating(type);

            return this.SingleObject(ratings);
        }
    }
}
