using System.Threading.Tasks;
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

        [HttpGet("{personId}")]
        public async Task<IActionResult> Get(int personId)
        {
            var person = await _personsService.GetPersonAsync(personId);

            if (person == null)
                return this.ErrorObject(404, $"Person (id: {personId}) is not found");

            return this.SingleObject(200, person);
        }

        [HttpGet("{personId}/editions")]
        public async Task<IActionResult> GetEditions(int personId)
        {
            var editions = await _personsService.GetPersonEditionsAsync(personId);

            if (editions == null)
                return this.ErrorObject(404, $"Person (id: {personId}) doesn't contain editions");

            return this.PageObject(editions, editions.Count);
        }

        [HttpGet("{personId}/awards")]
        public async Task<IActionResult> GetAwards(int personId)
        {
            var awards = await _personsService.GetPersonAwardsAsync(personId);

            if (awards == null)
                return this.ErrorObject(404, $"Person (id: {personId}) doesn't contain awards");

            return this.PageObject(awards, awards.Count);
        }
    }
}
