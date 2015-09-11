using System.Collections.Generic;
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
    public class AwardsController : Controller
    {
        private readonly IAwardsService _awardsService;

        public AwardsController(IAwardsService awardsService)
        {
            _awardsService = awardsService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<AwardResponse>))]
        public async Task<IActionResult> Index(AwardRequest request)
        {
            var awards = await _awardsService.GetAwardsAsync(request);

            return this.PageObject(awards.Values, awards.TotalRows, request.Limit, request.Offset);
        }

        [HttpGet("{id}")]
        [Produces(typeof(AwardResponse))]
        [SwaggerResponse(404, "Award is not found")]
        public async Task<IActionResult> Get(int id)
        {
            var award = await _awardsService.GetAwardAsync(id);

            if (award == null)
                return this.ErrorObject(404, $"Award (id: {id}) is not found");

            return this.SingleObject(award);
        }
    }
}
