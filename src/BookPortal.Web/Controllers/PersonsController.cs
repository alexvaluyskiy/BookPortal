using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Web.Models;
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
        public async Task<IActionResult> Index(PersonRequest request)
        {
            var persons = await _personsService.GetPeopleAsync(request);

            var totalrows = await _personsService.GetPeopleCountsAsync(request);

            return this.PageObject(persons, totalrows, request.Limit, request.Offset);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personsService.GetPersonAsync(id);

            if (person == null)
                return this.ErrorObject(404, $"Person (id: {id}) is not found");

            return this.SingleObject(200, person);
        }
    }
}
