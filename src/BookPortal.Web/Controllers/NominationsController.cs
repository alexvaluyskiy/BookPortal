using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/awards/{awardid}/[controller]")]
    public class NominationsController : Controller
    {
        private readonly NominationsService _nominationsService;

        public NominationsController(NominationsService nominationsService)
        {
            _nominationsService = nominationsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int awardId)
        {
            var nominations = await _nominationsService.GetNominationsAsync(awardId);

            return new WrappedObjectResult(nominations);
        }

        [HttpGet("{nominationId}")]
        public async Task<IActionResult> Get(int awardId, int nominationId)
        {
            var nomination = await _nominationsService.GetNominationAsync(awardId, nominationId);

            return new WrappedObjectResult(nomination);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Nomination request)
        {
            Nomination nomination = await _nominationsService.AddNominationAsync(request);

            if (nomination == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }

        [HttpPut("{nominationId}")]
        public async Task<IActionResult> Put(int nominationId, [FromBody]Nomination request)
        {
            Nomination nomination = await _nominationsService.UpdateNominationAsync(nominationId, request);

            if (nomination == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }

        [HttpDelete("{nominationId}")]
        public async Task<IActionResult> Delete(int nominationId)
        {
            Nomination nomination = await _nominationsService.DeleteNominationAsync(nominationId);

            if (nomination == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }
    }
}
