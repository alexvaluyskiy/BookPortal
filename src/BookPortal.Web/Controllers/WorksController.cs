using System.Threading.Tasks;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/persons/{personId}/[controller]")]
    public class WorksController : Controller
    {
        private readonly WorksService _worksService;

        public WorksController(WorksService worksService)
        {
            _worksService = worksService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int personId)
        {
            var works = await _worksService.GetWorksAsync(personId);

            return this.PageObject(200, works);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var work = await _worksService.GetWorkAsync(id);

            if (work == null)
            {
                return this.ErrorObject(404, $"Work (id: {id}) is not found");
            }

            return this.SingleObject(200, work);
        }
    }
}
