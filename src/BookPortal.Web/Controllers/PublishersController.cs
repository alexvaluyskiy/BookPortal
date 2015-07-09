using System.Threading.Tasks;
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
        public async Task<IActionResult> Get(int publisherId)
        {
            var publisher = await _publishersService.GetPublisherAsync(publisherId);

            if (publisher == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) is not found");

            return this.SingleObject(publisher);
        }

        [HttpGet("{publisherId}/editions")]
        public async Task<IActionResult> GetEditions(int publisherId)
        {
            var editions = await _publishersService.GetPublisherEditionsAsync(publisherId);

            if (editions == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain editions");

            return this.SingleObject(editions);
        }

        [HttpGet("{publisherId}/series")]
        public async Task<IActionResult> GetSeries(int publisherId)
        {
            var series = await _publishersService.GetPublisherSeriesAsync(publisherId);

            if (series == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain series");

            return this.SingleObject(series);
        }

        [HttpGet("{publisherId}/awards")]
        public async Task<IActionResult> GetAwards(int publisherId)
        {
            var awards = await _publishersService.GetPublisherAwardsAsync(publisherId);

            if (awards == null)
                return this.ErrorObject(404, $"Publisher (id: {publisherId}) doesn't contain awards");

            return this.SingleObject(awards);
        }
    }
}
