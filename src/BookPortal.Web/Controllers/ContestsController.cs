using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/awards/{awardid}/[controller]")]
    public class ContestsController : Controller
    {
        private readonly ContestsService _contestsService;

        public ContestsController(ContestsService contestsService)
        {
            _contestsService = contestsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int awardId)
        {
            var contests = await _contestsService.GetContestsAsync(awardId);

            return new WrappedObjectResult(contests);
        }

        [HttpGet("{contestId}")]
        public async Task<IActionResult> Get(int awardId, int contestId)
        {
            var contest = await _contestsService.GetContestAsync(awardId, contestId);

            return new WrappedObjectResult(contest);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Contest request)
        {
            Contest contest = await _contestsService.AddContestAsync(request);

            if (contest == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }

        [HttpPut("{contestId}")]
        public async Task<IActionResult> Put(int contestId, [FromBody]Contest request)
        {
            Contest contest = await _contestsService.UpdateContestAsync(contestId, request);

            if (contest == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }

        [HttpDelete("{contestId}")]
        public async Task<IActionResult> Delete(int contestId)
        {
            Contest contest = await _contestsService.DeleteContestAsync(contestId);

            if (contest == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }
    }
}
