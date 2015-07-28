using System.Threading.Tasks;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class EditionsController : Controller
    {
        private readonly EditionsService _editionsService;

        public EditionsController(EditionsService editionsService)
        {
            _editionsService = editionsService;
        }

        [HttpGet("{editionId}")]
        public async Task<IActionResult> Get(int editionId)
        {
            var edition = await _editionsService.GetEditionAsync(editionId);

            if (edition == null)
                return this.ErrorObject(404, $"Edition (id: {editionId}) is not found");

            return this.SingleObject(edition);
        }
    }
}
