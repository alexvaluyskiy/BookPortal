﻿using System.Collections.Generic;
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
    public class PersonsController : Controller
    {
        private readonly IAwardsService _awardsService;
        private readonly IPersonsService _personsService;
        private readonly IEditionsService _editionsService;
        private readonly IReviewsService _reviewsService;
        private readonly IWorksService _worksService;
        private readonly IGenresService _genresService;

        public PersonsController(
            IAwardsService awardsService,
            IPersonsService personsService,
            IEditionsService editionsService,
            IReviewsService reviewsService,
            IWorksService worksService,
            IGenresService genresService)
        {
            _awardsService = awardsService;
            _personsService = personsService;
            _editionsService = editionsService;
            _reviewsService = reviewsService;
            _worksService = worksService;
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
        [SwaggerResponse(404, "Person is not found")]
        public async Task<IActionResult> Get(int personId)
        {
            var person = await _personsService.GetPersonAsync(personId);

            if (person == null)
                return this.ErrorObject(404, $"Person (id: {personId}) is not found");

            return this.SingleObject(person);
        }

        [HttpGet("{personId}/works")]
        [Produces(typeof(IEnumerable<WorkResponse>))]
        public async Task<IActionResult> GetWorks(int personId, int userId = 0)
        {
            var works = await _worksService.GetWorksAsync(personId, userId);

            if (works == null)
                return this.ErrorObject(404, $"Person (id: {personId}) doesn't contain works");

            return this.PageObject(works.Values, works.TotalRows);
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
        [Produces(typeof(GenreWorkResponse))]
        public async Task<IActionResult> GetGenres(int personId)
        {
            var genres = await _genresService.GetAuthorGenres(personId);

            if (genres == null)
                return this.ErrorObject(404, $"Person (id: {personId}) doesn't contain genres");

            return this.PageObject(genres.Values, genres.TotalRows);
        }
    }
}
