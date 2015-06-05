using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class AwardsController : Controller
    {
        private readonly AwardsService _awardsService;

        public AwardsController(AwardsService awardsService)
        {
            _awardsService = awardsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AwardRequest request)
        {
            var awards = await _awardsService.GetAwardsAsync(request);

            var totalrows = await _awardsService.GetAwardCountsAsync(request);

            return this.PageObject(awards, totalrows, request.Limit, request.Offset);
        }

        [HttpGet("{id}", Name = "GetAward")]
        public async Task<IActionResult> Get(int id)
        {
            var award = await _awardsService.GetAwardAsync(id);

            if (award == null)
                return this.ErrorObject(404, $"Award (id: {id}) is not found");

            return this.SingleObject(200, award);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Award request)
        {
            AwardResponse award = await _awardsService.AddAwardAsync(request);

            if (award == null)
                return this.ErrorObject(400);

            return new CreatedAtRouteResult("GetAward", new {id = award.Id}, award);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Award request)
        {
            AwardResponse award = await _awardsService.UpdateAwardAsync(id, request);

            if (award == null)
                return this.ErrorObject(400);
                
            return new HttpStatusCodeResult(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            AwardResponse award = await _awardsService.DeleteAwardAsync(id);

            if (award == null)
                return this.ErrorObject(400);

            return new HttpStatusCodeResult(204);
        }
    }
}
