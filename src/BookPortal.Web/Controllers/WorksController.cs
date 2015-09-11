using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using BookPortal.Web.Services.Interfaces;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class WorksController : Controller
    {
        private readonly IAwardsService _awardsService;
        private readonly IWorksService _worksService;
        private readonly IEditionsService _editionsService;
        private readonly IReviewsService _reviewsService;
        private readonly ITranslationsService _translationsService;
        private readonly IGenresService _genresService;

        public WorksController(
            IAwardsService awardsService,
            IWorksService worksService,
            IEditionsService editionsService,
            IReviewsService reviewsService,
            ITranslationsService translationsService,
            IGenresService genresService)
        {
            _awardsService = awardsService;
            _worksService = worksService;
            _editionsService = editionsService;
            _reviewsService = reviewsService;
            _translationsService = translationsService;
            _genresService = genresService;
        }

        [HttpGet("{workId}")]
        [Produces(typeof(WorkResponse))]
        [SwaggerResponse(404, "Work is not found")]
        public async Task<IActionResult> Get(int workId)
        {
            var work = await _worksService.GetWorkAsync(workId);

            if (work == null)
            {
                return this.ErrorObject(404, $"Work (id: {workId}) is not found");
            }

            return this.SingleObject(work);
        }

        [HttpGet("{workId}/mark")]
        [Produces(typeof(MarkResponse))]
        [SwaggerResponse(404, "Work is not found")]
        public async Task<IActionResult> GetWorkMark(int workId, [FromQuery]int userId)
        {
            var work = await _worksService.GetWorkMarkAsync(workId, userId);

            if (work == null)
            {
                return this.ErrorObject(404, $"Work (id: {workId}) is not found");
            }

            return this.SingleObject(work);
        }

        [HttpGet("{workId}/awards")]
        [Produces(typeof(IEnumerable<AwardItemResponse>))]
        public async Task<IActionResult> GetAwards(int workId)
        {
            var awards = await _awardsService.GetWorkAwardsAsync(workId);

            if (awards == null)
                return this.ErrorObject(404, $"Work (id: {workId}) doesn't contain awards");

            return this.PageObject(awards, awards.Count);
        }

        [HttpGet("{workId}/editions")]
        [Produces(typeof(IEnumerable<EditionResponse>))]
        public async Task<IActionResult> GetEditions(int workId)
        {
            var editions = await _editionsService.GetEditionsByWorkAsync(workId);

            if (editions == null)
                return this.ErrorObject(404, $"Work (id: {workId}) doesn't contain editions");

            return this.PageObject(editions.Values, editions.TotalRows);
        }

        [HttpGet("{workId}/reviews")]
        [Produces(typeof(IEnumerable<ReviewResponse>))]
        public async Task<IActionResult> IndexWork(ReviewWorkRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsByWorkAsync(reviewRequest);

            if (reviews == null)
                return this.ErrorObject(404, $"Work (id: {reviewRequest.WorkId}) doesn't contain reviews");

            return this.PageObject(reviews.Values, reviews.TotalRows, reviewRequest.Limit, reviewRequest.Offset);
        }

        [HttpGet("{workId}/translations")]
        [Produces(typeof(IEnumerable<TranslationResponse>))]
        public async Task<IActionResult> GetTranslations(int workId)
        {
            var translations = await _translationsService.GetWorkTranslationsAsync(workId);

            if (translations == null)
                return this.ErrorObject(404, $"Work (id: {workId}) doesn't contain translations");

            return this.PageObject(translations);
        }

        [HttpGet("{workId}/genres")]
        [Produces(typeof(IEnumerable<GenreWorkResponse>))]
        public async Task<IActionResult> GetGenres(int workId)
        {
            var genres = await _genresService.GetWorkGenres(workId);

            if (genres == null)
                return this.ErrorObject(404, $"Work (id: {workId}) doesn't contain genres");

            return this.PageObject(genres.Values, genres.TotalRows);
        }
    }
}
