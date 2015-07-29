using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/persons/{personId}/[controller]")]
    public class WorksController : Controller
    {
        private readonly WorksService _worksService;

        public WorksController(WorksService worksService)
        {
            _worksService = worksService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<WorkResponse>))]
        public async Task<IActionResult> Index(int personId, string sortMode)
        {
            var works = await _worksService.GetWorksAsync(personId, sortMode);

            return this.PageObject(works);
        }

        [HttpGet("{id}")]
        [Produces(typeof(WorkResponse))]
        public async Task<IActionResult> Get(int id)
        {
            var work = await _worksService.GetWorkAsync(id);

            if (work == null)
            {
                return this.ErrorObject(404, $"Work (id: {id}) is not found");
            }

            return this.SingleObject(work);
        }

        [HttpGet("{workId}/awards")]
        [Produces(typeof(IEnumerable<AwardItemResponse>))]
        public async Task<IActionResult> GetAwards(int workId)
        {
            var awards = await _worksService.GetWorkAwardsAsync(workId);

            if (awards == null)
                return this.ErrorObject(404, $"Work (id: {workId}) doesn't contain awards");

            return this.PageObject(awards, awards.Count);
        }

        [HttpGet("{workId}/editions")]
        [Produces(typeof(IEnumerable<EditionResponse>))]
        public async Task<IActionResult> GetEditions(int workId)
        {
            var editions = await _worksService.GetWorkEditionsAsync(workId);

            if (editions == null)
                return this.ErrorObject(404, $"Work (id: {workId}) doesn't contain editions");

            return this.PageObject(editions, editions.Count);
        }

        [HttpGet("{workId}/translations")]
        [Produces(typeof(IEnumerable<TranslationResponse>))]
        public async Task<IActionResult> GetTranslations(int workId)
        {
            var translations = await _worksService.GetWorkTranslationsAsync(workId);

            if (translations == null)
                return this.ErrorObject(404, $"Work (id: {workId}) doesn't contain translations");

            return this.PageObject(translations);
        }
    }
}
