using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Infrastructure;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class NominationsController : Controller
    {
        private readonly NominationsService _nominationsService;

        public NominationsController(NominationsService nominationsService)
        {
            _nominationsService = nominationsService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Nomination request)
        {
            if (!ModelState.IsValid)
                return new WrappedErrorResult(400);

            Nomination nomination = await _nominationsService.AddNominationAsync(request);

            if (nomination == null)
                return new WrappedErrorResult(400);

            return new CreatedAtActionResult("Index", "Nominations", new { id = nomination.Id }, nomination);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Nomination request)
        {
            if (!ModelState.IsValid)
                return new WrappedErrorResult(400);

            Nomination nomination = await _nominationsService.UpdateNominationAsync(id, request);

            if (nomination == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Nomination nomination = await _nominationsService.DeleteNominationAsync(id);

            if (nomination == null)
                return new WrappedErrorResult(400);

            return new NoContentResult();
        }
    }
}
