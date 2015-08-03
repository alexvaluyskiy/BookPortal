﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace BookPortal.Web.Controllers
{
    [Route("api/awards/{awardid}/[controller]")]
    public class ContestsController : Controller
    {
        private readonly ContestsService _contestsService;

        public ContestsController(ContestsService contestsService)
        {
            _contestsService = contestsService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<ContestResponse>))]
        public async Task<IActionResult> Index(int awardId)
        {
            var contests = await _contestsService.GetContestsAsync(awardId);

            return this.PageObject(contests.Values, contests.TotalRows);
        }

        [HttpGet("{contestId}")]
        [Produces(typeof(ContestResponse))]
        [SwaggerResponse(404, "Contest is not found")]
        public async Task<IActionResult> Get(int awardId, int contestId)
        {
            var contest = await _contestsService.GetContestAsync(awardId, contestId);

            if (contest == null)
                return this.ErrorObject(404, $"Contest (id: {contestId}) is not found");

            return this.SingleObject(contest);
        }
    }
}
