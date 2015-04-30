using System.Threading.Tasks;
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
        public async Task<IActionResult> Get()
        {
            var awards = await _awardsService.GetAwardsAsync();

            return new ObjectResult(awards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var award = await _awardsService.GetAwardAsync(id);

            if (award == null)
                return new HttpNotFoundResult();

            return new ObjectResult(award);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AwardRequest request)
        {
            Award award = await _awardsService.AddAwardAsync(request);

            if (award == null)
                return new BadRequestResult();

            return new HttpStatusCodeResult(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AwardRequest request)
        {
            Award award = await _awardsService.UpdateAwardAsync(id, request);

            if (award == null)
                return new BadRequestResult();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Award award = await _awardsService.DeleteAwardAsync(id);

            if (award == null)
                return new BadRequestResult();

            return new NoContentResult();
        }
    }
}
