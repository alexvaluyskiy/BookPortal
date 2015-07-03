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
            var languages = await _languagesService.GetLanguagesAsync();

            return this.PageObject(200, languages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var language = await _languagesService.GetLanguageAsync(id);

            if (language == null)
                return this.ErrorObject(404, $"Language (id: {id}) is not found");

            return this.SingleObject(200, language);
        }
    }
}
