using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Ratings.Models;
using BookPortal.Ratings.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Ratings.Controllers
{
    public class GenresController : Controller
    {
        private readonly GenresService _genresService;

        public GenresController(GenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpGet("api/persons/{personId}/genres")]
        [Produces(typeof(IEnumerable<AuthorRatingResponse>))]
        public async Task<IActionResult> GetAuthorGenres(int personId)
        {
            var ratings = await _genresService.GetAuthorGenres(personId);

            return this.PageObject(ratings.Values, ratings.TotalRows);
        }

        [HttpGet("api/works/{workId}/genres")]
        [Produces(typeof(IEnumerable<WorkRatingResponse>))]
        public async Task<IActionResult> GetWorkGenres(int workId)
        {
            var ratings = await _genresService.GetWorkGenres(workId);

            return this.PageObject(ratings.Values, ratings.TotalRows);
        }
    }
}
