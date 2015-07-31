using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly AwardsService _awardsService;
        private readonly PersonsService _personsService;
        private readonly EditionsService _editionsService;
        private readonly ReviewsService _reviewsService;
        private readonly GenresService _genresService;

        public PersonsController(
            AwardsService awardsService,
            PersonsService personsService,
            EditionsService editionsService,
            ReviewsService reviewsService,
            GenresService genresService)
        {
            _awardsService = awardsService;
            _personsService = personsService;
            _editionsService = editionsService;
            _reviewsService = reviewsService;
            _genresService = genresService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<PersonResponse>))]
        public async Task<IActionResult> Index(PersonRequest request)
        {
            var persons = await _personsService.GetPeopleAsync(request);

            return this.PageObject(persons.Values, persons.TotalRows, request.Limit, request.Offset);
        }

        [HttpGet("{personId}")]
        [Produces(typeof(PersonResponse))]
        public async Task<IActionResult> Get(int personId)
        {
            var person = await _personsService.GetPersonAsync(personId);

            if (person == null)
                return this.ErrorObject(404, $"Person (id: {personId}) is not found");

            return this.SingleObject(person);
        }

        [HttpGet("{personId}/editions")]
        [Produces(typeof(IEnumerable<EditionResponse>))]
        public async Task<IActionResult> GetEditions(int personId)
        {
            var editions = await _editionsService.GetEditionsByPersonAsync(personId);

            if (editions == null)
                return this.ErrorObject(404, $"Person (id: {personId}) doesn't contain editions");

            return this.PageObject(editions.Values, editions.TotalRows);
        }

        [HttpGet("{personId}/awards")]
        [Produces(typeof(IEnumerable<AwardItemResponse>))]
        public async Task<IActionResult> GetAwards(int personId)
        {
            var awards = await _awardsService.GetPersonAwardsAsync(personId);

            if (awards == null)
                return this.ErrorObject(404, $"Person (id: {personId}) doesn't contain awards");

            return this.PageObject(awards, awards.Count);
        }

        [HttpGet("{personId}/reviews")]
        [Produces(typeof(IEnumerable<ReviewResponse>))]
        public async Task<IActionResult> GetReviews(ReviewPersonRequest reviewRequest)
        {
            var reviews = await _reviewsService.GetReviewsByPersonAsync(reviewRequest);

            if (reviews == null)
                return this.ErrorObject(404, $"Person (id: {reviewRequest.PersonId}) doesn't contain reviews");

            return this.PageObject(reviews.Values, reviews.TotalRows, reviewRequest.Limit, reviewRequest.Offset);
        }

        [HttpGet("{personId}/genres")]
        public async Task<IActionResult> GetGenres(int personId)
        {
            var genres = await _genresService.GetAuthorGenres(personId);

            if (genres == null)
                return this.ErrorObject(404, $"Person (id: {personId}) doesn't contain genres");

            return this.PageObject(genres.Values, genres.TotalRows);
        }
    }
}
