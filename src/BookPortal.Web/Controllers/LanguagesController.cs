using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Repositories;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class LanguagesController : Controller
    {
        private readonly LanguagesRepository _languagesRepository;

        public LanguagesController(LanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<LanguageResponse>))]
        public async Task<IActionResult> Index()
        {
            var languages = await _languagesRepository.GetLanguagesAsync();

            return this.PageObject(languages.Values, languages.TotalRows);
        }

        [HttpGet("{id}")]
        [Produces(typeof(LanguageResponse))]
        [SwaggerResponse(404, "Language is not found")]
        public async Task<IActionResult> Get(int id)
        {
            var language = await _languagesRepository.GetLanguageAsync(id);

            if (language == null)
                return this.ErrorObject(404, $"Language (id: {id}) is not found");

            return this.SingleObject(language);
        }
    }
}
