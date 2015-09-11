using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using BookPortal.Web.Services.Interfaces;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class WorkTypesController : Controller
    {
        private readonly IWorkTypesService _workTypesService;

        public WorkTypesController(IWorkTypesService workTypesService)
        {
            _workTypesService = workTypesService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<WorkTypeResponse>))]
        public async Task<IActionResult> Index()
        {
            var workTypes = await _workTypesService.GetWorkTypesListAsync();

            return this.PageObject(workTypes.Values, workTypes.TotalRows);
        }

        [HttpGet("{workTypeId}")]
        [Produces(typeof(WorkTypeResponse))]
        [SwaggerResponse(404, "Work Type is not found")]
        public async Task<IActionResult> Get(int workTypeId)
        {
            var workType = await _workTypesService.GetWorkTypeAsync(workTypeId);

            if (workType == null)
                return this.ErrorObject(404, $"WorkType (id: {workTypeId}) is not found");

            return this.SingleObject(workType);
        }
    }
}
