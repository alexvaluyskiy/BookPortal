﻿using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/awards/{awardid}/contests/{contestid}/[controller]")]
    public class ContestsWorksController : Controller
    {
        private readonly ContestsWorksService _contestsWorksService;

        public ContestsWorksController(ContestsWorksService contestsWorksService)
        {
            _contestsWorksService = contestsWorksService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int contestId)
        {
            var contestWorks = await _contestsWorksService.GetContestsWorksAsync(contestId);

            return this.PageObject(200, contestWorks);
        }

        [HttpGet("{contestWorkId}")]
        public async Task<IActionResult> Get(int contestId, int contestWorkId)
        {
            var contestWork = await _contestsWorksService.GetContestWorkAsync(contestId, contestWorkId);

            return this.SingleObject(200, contestWork);
        }
    }
}
