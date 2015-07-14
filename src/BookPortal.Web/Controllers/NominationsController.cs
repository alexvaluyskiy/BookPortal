using System.Threading.Tasks;
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

            return this.PageObject(200, nominations);
        }

        [HttpGet("{nominationId}")]
        public async Task<IActionResult> Get(int awardId, int nominationId)
        {
            var nomination = await _nominationsService.GetNominationAsync(awardId, nominationId);

            return this.SingleObject(200, nomination);
        }
    }
}
