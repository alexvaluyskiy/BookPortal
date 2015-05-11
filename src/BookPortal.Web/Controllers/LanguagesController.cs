using System.Threading.Tasks;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class LanguagesController : Controller
    {
        private readonly LanguagesService _languagesService;

        public LanguagesController(LanguagesService languagesService)
        {
            _languagesService = languagesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var countries = await _languagesService.GetLanguagesAsync();

            return new WrappedObjectResult(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _languagesService.GetLanguageAsync(id);

            if (country == null)
                return new WrappedErrorResult(404, $"Language (id: {id}) is not found");

            return new WrappedObjectResult(country);
        }
    }
}
