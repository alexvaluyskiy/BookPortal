using System.Threading.Tasks;
using BookPortal.Responses.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Responses.Controllers
{
    [Route("api/[controller]")]
    public class ResponsesController : Controller
    {
        private readonly ResponsesService _responsesService;

        public ResponsesController(ResponsesService responsesService)
        {
            _responsesService = responsesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var persons = await _responsesService.GetResponsesAsync();

            return new ObjectResult(persons);
        }

        [HttpGet("{responseid}")]
        public async Task<IActionResult> Get(int responseId)
        {
            var person = await _responsesService.GetResponseAsync(responseId);

            if (person == null)
                return new HttpStatusCodeResult(404);

            return new ObjectResult(person);
        }
    }
}
