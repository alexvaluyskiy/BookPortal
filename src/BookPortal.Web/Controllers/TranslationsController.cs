using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using BookPortal.Web.Services.Interfaces;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/persons/{personId}/[controller]")]
    public class TranslationsController : Controller
    {
        private readonly ITranslationsService _translationsService;

        public TranslationsController(ITranslationsService translationsService)
        {
            _translationsService = translationsService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<TranslationResponse>))]
        public async Task<IActionResult> TranslationsList(TranslationRequest request)
        {
            var translations = await _translationsService.GetTranslationsAsync(request);

            return this.PageObject(translations, translations.Count, 0, 0);
        }

        [HttpGet("{translationWorkId}/editions")]
        [Produces(typeof(IEnumerable<EditionResponse>))]
        public async Task<IActionResult> GetEditions(int personId, int translationWorkId)
        {
            var translations = await _translationsService.GetTranslationEditionsAsync(translationWorkId);

            return this.PageObject(translations, translations.Count, 0, 0);
        }
    }
}
