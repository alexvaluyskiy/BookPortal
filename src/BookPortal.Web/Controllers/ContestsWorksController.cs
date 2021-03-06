﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using BookPortal.Web.Services.Interfaces;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/awards/{awardid}/contests/{contestid}/[controller]")]
    public class ContestsWorksController : Controller
    {
        private readonly IContestsWorksService _contestsWorksService;

        public ContestsWorksController(IContestsWorksService contestsWorksService)
        {
            _contestsWorksService = contestsWorksService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<ContestWorkResponse>))]
        [SwaggerOperation("GetContestWorks", Tags = new[] { "Awards" })]
        public async Task<IActionResult> Index(int awardId, int contestId)
        {
            var contestWorks = await _contestsWorksService.GetContestsWorksAsync(contestId);

            return this.PageObject(contestWorks);
        }

        [HttpGet("{contestWorkId}")]
        [Produces(typeof(ContestWorkResponse))]
        [SwaggerResponse(404, "Contest work is not found")]
        [SwaggerOperation("GetContestWorkById", Tags = new[] { "Awards" })]
        public async Task<IActionResult> Get(int awardId, int contestId, int contestWorkId)
        {
            var contestWork = await _contestsWorksService.GetContestWorkAsync(contestId, contestWorkId);

            if (contestWork == null)
                return this.ErrorObject(404, $"Contest work (id: {contestWorkId}) is not found");

            return this.SingleObject(contestWork);
        }
    }
}
