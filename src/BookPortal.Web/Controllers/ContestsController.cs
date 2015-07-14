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

            return this.PageObject(200, contests);
        }

        [HttpGet("{contestId}")]
        public async Task<IActionResult> Get(int awardId, int contestId)
        {
            var contest = await _contestsService.GetContestAsync(awardId, contestId);

            return this.SingleObject(200, contest);
        }
    }
}
