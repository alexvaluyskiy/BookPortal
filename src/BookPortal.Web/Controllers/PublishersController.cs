using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class PublishersController : Controller
    {
        private readonly PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpGet("{publisherId}")]
        [Produces(typeof(PublisherResponse))]
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
            var editions = await _publishersService.GetPublisherEditionsAsync(publisherId);

            if (editions == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain editions");

            return this.PageObject(editions, editions.Count);
        }

        [HttpGet("{publisherId}/series")]
        [Produces(typeof(IEnumerable<SerieResponse>))]
        public async Task<IActionResult> GetSeries(int publisherId)
        {
            var series = await _publishersService.GetPublisherSeriesAsync(publisherId);

            if (series == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain series");

            return this.PageObject(series, series.Count);
        }

        [HttpGet("{publisherId}/awards")]
        [Produces(typeof(IEnumerable<AwardItemResponse>))]
        public async Task<IActionResult> GetAwards(int publisherId)
        {
            var awards = await _publishersService.GetPublisherAwardsAsync(publisherId);

            if (awards == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain awards");

            return this.PageObject(awards, awards.Count);
        }
    }
}
