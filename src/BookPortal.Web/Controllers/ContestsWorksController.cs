using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models;
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
        [Produces(typeof(IEnumerable<ContestWorkResponse>))]
        public async Task<IActionResult> Index(int contestId)
        {
            var contestWorks = await _contestsWorksService.GetContestsWorksAsync(contestId);

            return this.PageObject(contestWorks);
        }

        [HttpGet("{contestWorkId}")]
        [Produces(typeof(ContestWorkResponse))]
        public async Task<IActionResult> Get(int contestId, int contestWorkId)
        {
            var contestWork = await _contestsWorksService.GetContestWorkAsync(contestId, contestWorkId);

            return this.SingleObject(contestWork);
        }
    }
}
