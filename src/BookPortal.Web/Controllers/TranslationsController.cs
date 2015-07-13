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

        [HttpGet("")]
        public async Task<IActionResult> TranslationsList(TranslationRequest request)
        {
            var translations = await _translationsService.GetTranslationsAsync(request);

            return this.PageObject(translations, translations.Count, 0, 0);
        }

        [HttpGet("{translationWorkId}/editions")]
        public async Task<IActionResult> GetEditions(int translationWorkId)
        {
            var translations = await _translationsService.GetTranslationEditionsAsync(translationWorkId);

            return this.PageObject(translations, translations.Count, 0, 0);
        }
    }
}
