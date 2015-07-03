using System.Threading.Tasks;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/persons/{personId}/[controller]")]
    public class TranslationsController : Controller
    {
        private readonly TranslationsService _translationsService;

        public TranslationsController(TranslationsService translationsService)
        {
            _translationsService = translationsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(TranslationRequest request)
        {
            var translations = await _translationsService.GetTranslationsAsync(request);

            return this.SingleObject(200, translations);
        }
    }
}
