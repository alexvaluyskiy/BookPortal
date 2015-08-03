﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/awards/{awardid}/[controller]")]
    public class NominationsController : Controller
    {
        private readonly NominationsService _nominationsService;

        public NominationsController(NominationsService nominationsService)
        {
            _nominationsService = nominationsService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<NominationResponse>))]
        public async Task<IActionResult> Index(int awardId)
        {
            var nominations = await _nominationsService.GetNominationsAsync(awardId);

            return this.PageObject(nominations);
        }

        [HttpGet("{nominationId}")]
        [Produces(typeof(NominationResponse))]
        [SwaggerResponse(404, "Nomination is not found")]
        public async Task<IActionResult> Get(int awardId, int nominationId)
        {
            var nomination = await _nominationsService.GetNominationAsync(awardId, nominationId);

            if (nomination == null)
                return this.ErrorObject(404, $"Nomination (id: {nominationId}) is not found");

            return this.SingleObject(nomination);
        }
    }
}
