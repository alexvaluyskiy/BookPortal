using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/awards/{awardid}/contests/{contestid}/[controller]")]
    public class ContestsWorksController : Controller
    {
        private readonly ContestsWorksService _contestsWorksService;

        public ContestsWorksController(ContestsWorksService contestsWorksService)
        {
            _contestsWorksService = contestsWorksService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int contestId)
        {
            var contestWorks = await _contestsWorksService.GetContestsWorksAsync(contestId);

            return this.PageObject(200, contestWorks);
        }

        [HttpGet("{contestWorkId}")]
        public async Task<IActionResult> Get(int contestId, int contestWorkId)
        {
            var contestWork = await _contestsWorksService.GetContestWorkAsync(contestId, contestWorkId);

            return this.SingleObject(200, contestWork);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ContestWork request)
        {
            ContestWork contestWork = await _contestsWorksService.AddContestWorkAsync(request);

            if (contestWork == null)
                return this.ErrorObject(400);

            return new HttpStatusCodeResult(204);
        }

        [HttpPut("{contestWorkId}")]
        public async Task<IActionResult> Put(int contestWorkId, [FromBody]ContestWork request)
        {
            ContestWork contestWork = await _contestsWorksService.UpdateContestWorkAsync(contestWorkId, request);

            if (contestWork == null)
                return this.ErrorObject(400);

            return new HttpStatusCodeResult(204);
        }

        [HttpDelete("{contestWorkId}")]
        public async Task<IActionResult> Delete(int contestWorkId)
        {
            ContestWork contestWork = await _contestsWorksService.DeleteContestWorkAsync(contestWorkId);

            if (contestWork == null)
                return this.ErrorObject(400);

            return new HttpStatusCodeResult(204);
        }
    }
}
