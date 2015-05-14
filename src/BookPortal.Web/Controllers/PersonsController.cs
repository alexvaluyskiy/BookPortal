using System.Threading.Tasks;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly PersonsService _personsService;

        public PersonsController(PersonsService personsService)
        {
            _personsService = personsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var countries = await _personsService.GetPersonsAsync();

            return new WrappedObjectResult(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _personsService.GetPersonAsync(id);

            if (country == null)
                return new WrappedErrorResult(404, $"Person (id: {id}) is not found");

            return new WrappedObjectResult(country);
        }
    }
}
