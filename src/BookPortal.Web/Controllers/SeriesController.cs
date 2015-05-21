using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private readonly SeriesService _seriesService;

        public SeriesController(SeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int publisherId)
        {
            var series = await _seriesService.GetSeriesAsync(publisherId);

            return this.PageObject(200, series);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var serie = await _seriesService.GetSerieAsync(id);

            if (serie == null)
                return this.ErrorObject(404, $"Serie (id: {id}) is not found");

            return this.SingleObject(200, serie);
        }
    }
}
