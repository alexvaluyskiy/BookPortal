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
            var persons = await _personsService.GetPersonsAsync();

            return new WrappedObjectResult(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personsService.GetPersonAsync(id);

            if (person == null)
                return new WrappedErrorResult(404, $"Person (id: {id}) is not found");

            return new WrappedObjectResult(person);
        }
    }
}
