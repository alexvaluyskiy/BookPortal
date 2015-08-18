using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Repositories;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly GenresService _genresService;

        public GenresController(GenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpGet("groups")]
        [Produces(typeof(IEnumerable<GenreWorksGroupResponse>))]
        public async Task<IActionResult> Groups()
        {
            var genreWorkGroups = await _genresService.GetGenreWorksGroups();

            return this.PageObject(genreWorkGroups.Values, genreWorkGroups.TotalRows);
        }
    }
}
