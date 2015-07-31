using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/persons/{personId}/[controller]")]
    public class WorksController : Controller
    {
        private readonly AwardsService _awardsService;
        private readonly WorksService _worksService;
        private readonly EditionsService _editionsService;
        private readonly ReviewsService _reviewsService;
        private readonly TranslationsService _translationsService;
        private readonly GenresService _genresService;

        public WorksController(
            AwardsService awardsService,
            WorksService worksService,
            EditionsService editionsService,
            ReviewsService reviewsService,
            TranslationsService translationsService,
            GenresService genresService)
        {
            _awardsService = awardsService;
            _worksService = worksService;
            _editionsService = editionsService;
            _reviewsService = reviewsService;
            _translationsService = translationsService;
            _genresService = genresService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<WorkResponse>))]
        public async Task<IActionResult> Index(int personId, string sortMode)
        {
            var works = await _worksService.GetWorksAsync(personId, sortMode);

            return this.PageObject(works);
        }

        [HttpGet("{id}")]
        [Produces(typeof(WorkResponse))]
        public async Task<IActionResult> Get(int id)
        {
            var work = await _worksService.GetWorkAsync(id);

            if (work == null)
            {
                return this.ErrorObject(404, $"Work (id: {id}) is not found");
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
        public async Task<IActionResult> GetGenres(int workId)
        {
            var ratings = await _genresService.GetWorkGenres(workId);

            return this.PageObject(ratings.Values, ratings.TotalRows);
        }
    }
}
