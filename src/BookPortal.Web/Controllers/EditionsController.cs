using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class EditionsController : Controller
    {
        private readonly EditionsService _editionsService;

        public EditionsController(EditionsService editionsService)
        {
            _editionsService = editionsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int workId)
        {
            var editions = await _editionsService.GetEditionsAsync(workId);

            return this.PageObject(200, editions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var edition = await _editionsService.GetEditionAsync(id);

            if (edition == null)
                return this.ErrorObject(404, $"Edition (id: {id}) is not found");

            return this.SingleObject(200, edition);
        }
    }
}
