using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class WorksController : Controller
    {
        private readonly WorksService _worksService;

        public WorksController(WorksService worksService)
        {
            _worksService = worksService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var countries = await _worksService.GetWorksAsync();

            return new WrappedObjectResult(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _worksService.GetWorkAsync(id);

            if (country == null)
                return new WrappedErrorResult(404, $"Work (id: {id}) is not found");

            return new WrappedObjectResult(country);
        }
    }
}
