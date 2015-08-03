using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private readonly SeriesService _seriesService;
        private readonly EditionsService _editionsService;

        public SeriesController(
            SeriesService seriesService,
            EditionsService editionsService)
        {
            _seriesService = seriesService;
            _editionsService = editionsService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<SerieResponse>))]
        public async Task<IActionResult> Index(int publisherId)
        {
            var series = await _seriesService.GetSeriesAsync(publisherId);

            return this.PageObject(series);
        }

        [HttpGet("{serieId}")]
        [Produces(typeof(SerieResponse))]
        [SwaggerResponse(404, "Serie is not found")]
        public async Task<IActionResult> Get(int serieId)
        {
            var serie = await _seriesService.GetSerieAsync(serieId);

            if (serie == null)
                return this.ErrorObject(404, $"Serie (id: {serieId}) is not found");

            return this.SingleObject(serie);
        }

        [HttpGet("{serieId}/editions")]
        [Produces(typeof(IEnumerable<EditionResponse>))]
        public async Task<IActionResult> GetEditions(SerieRequest request)
        {
            var editions = await _editionsService.GetEditionsBySerieAsync(request);

            if (editions == null)
                return this.ErrorObject(404, $"Serie (id: {request.SerieId}) editions are not found");

            return this.PageObject(editions.Values, editions.TotalRows, request.Limit, request.Offset);
        }

        [HttpGet("{serieId}/tree")]
        [Produces(typeof(SerieTreeItem))]
        public async Task<IActionResult> GetTree(int serieId)
        {
            var treeItem = await _seriesService.GetSerieTreeAsync(serieId);

            if (treeItem == null)
                return this.ErrorObject(404, $"Serie (id: {serieId}) tree is not found");

            return this.SingleObject(treeItem);
        }
    }
}
