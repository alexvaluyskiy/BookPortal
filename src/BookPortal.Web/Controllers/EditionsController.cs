using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using BookPortal.Web.Services.Interfaces;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class EditionsController : Controller
    {
        private readonly IEditionsService _editionsService;

        public EditionsController(IEditionsService editionsService)
        {
            _editionsService = editionsService;
        }

        [HttpGet("{editionId}")]
        [Produces(typeof(EditionResponse))]
        [SwaggerResponse(404, "Edition is not found")]
        public async Task<IActionResult> Get(int editionId)
        {
            var edition = await _editionsService.GetEditionAsync(editionId);

            if (edition == null)
                return this.ErrorObject(404, $"Edition (id: {editionId}) is not found");

            return this.SingleObject(edition);
        }
    }
}
