using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using BookPortal.Web.Services.Interfaces;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class PublishersController : Controller
    {
        private readonly IAwardsService _awardsService;
        private readonly IPublishersService _publishersService;
        private readonly IEditionsService _editionsService;
        private readonly ISeriesService _seriesService;

        public PublishersController(
            IAwardsService awardsService,
            IPublishersService publishersService,
            IEditionsService editionsService,
            ISeriesService seriesService)
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
