using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Ratings.Models;
using BookPortal.Ratings.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Ratings.Controllers
{
    public class MarksController : Controller
    {
        private readonly MarksService _marksService;

        public MarksController(MarksService marksService)
        {
            _marksService = marksService;
        }

        [HttpGet("api/persons/{personId}/marks")]
        [Produces(typeof(IEnumerable<MarkResponse>))]
        public async Task<IActionResult> GetMarksByWorkIds(List<int> ids, int userId)
        {
            var ratings = await _marksService.GetMarksByWorkIds(ids, userId);

            return this.PageObject(ratings.Values, ratings.TotalRows);
        }

        [HttpGet("api/works/{workId}/marks")]
        [Produces(typeof(IEnumerable<MarkResponse>))]
        public async Task<IActionResult> GetMarkByWork(int workId, int userId)
        {
            var marks = await _marksService.GetMarkByWork(workId, userId);

            return this.SingleObject(marks);
        }
    }
}
