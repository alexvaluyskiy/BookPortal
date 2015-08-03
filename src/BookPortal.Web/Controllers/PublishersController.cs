using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class PublishersController : Controller
    {
        private readonly AwardsService _awardsService;
        private readonly PublishersService _publishersService;
        private readonly EditionsService _editionsService;
        private readonly SeriesService _seriesService;

        public PublishersController(
            AwardsService awardsService,
            PublishersService publishersService,
            EditionsService editionsService,
            SeriesService seriesService)
        {
            _awardsService = awardsService;
            _publishersService = publishersService;
            _editionsService = editionsService;
            _seriesService = seriesService;
        }

        [HttpGet("{publisherId}")]
        [Produces(typeof(PublisherResponse))]
        [SwaggerResponse(404, "Publisher is not found")]
        public async Task<IActionResult> Get(int publisherId)
        {
            var publisher = await _publishersService.GetPublisherAsync(publisherId);

            if (publisher == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) is not found");

            return this.SingleObject(publisher);
        }

        [HttpGet("{publisherId}/editions")]
        [Produces(typeof(IEnumerable<EditionResponse>))]
        public async Task<IActionResult> GetEditions(int publisherId)
        {
            var editions = await _editionsService.GetEditionsByPublisherAsync(publisherId);

            if (editions == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain editions");

            return this.PageObject(editions.Values, editions.TotalRows);
        }

        [HttpGet("{publisherId}/series")]
        [Produces(typeof(IEnumerable<SerieResponse>))]
        public async Task<IActionResult> GetSeries(int publisherId)
        {
            var series = await _seriesService.GetSerieByPublisher(publisherId);

            if (series == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain series");

            return this.PageObject(series, series.Count);
        }

        [HttpGet("{publisherId}/awards")]
        [Produces(typeof(IEnumerable<AwardItemResponse>))]
        public async Task<IActionResult> GetAwards(int publisherId)
        {
            var awards = await _awardsService.GetPublisherAwardsAsync(publisherId);

            if (awards == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain awards");

            return this.PageObject(awards, awards.Count);
        }
    }
}
