using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Ratings.Domain;
using BookPortal.Ratings.Models;
using Microsoft.Data.Entity;

namespace BookPortal.Ratings.Services
{
    public class MarksService
    {
        private readonly RatingsContext _ratingsContext;

        public MarksService(RatingsContext ratingsContext)
        {
            _ratingsContext = ratingsContext;
        }

        public async Task<ApiObject<MarkResponse>> GetMarksByWorkIds(List<int> ids, int userId)
        {
            var query = _ratingsContext.Marks
                        .Where(c => ids.Contains(c.WorkId))
                        .GroupBy(c => c.WorkId)
                        .Select(c => new MarkResponse
                        {
                            WorkId = c.Key,
                            Rating = c.Average(d => d.MarkValue),
                            MarkCount = c.Count()
                        });

            var result = await query.ToListAsync();

            if (userId > 0)
            {
                var marks = await _ratingsContext.Marks
                            .Where(c => ids.Contains(c.WorkId) && c.UserId == userId)
                            .Select(c => new  {c.WorkId, c.MarkValue})
                            .ToListAsync();

                foreach (var mark in marks)
                {
                    var workMark = result.SingleOrDefault(c => c.WorkId == mark.WorkId);
                    if (workMark != null)
                        workMark.UserMark = mark.MarkValue;
                }
            }

            var apiObject = new ApiObject<MarkResponse>();
            apiObject.Values = result;
            apiObject.TotalRows = apiObject.Values.Count;

            return apiObject;
        }

        public async Task<MarkResponse> GetMarkByWork(int workId, int userId)
        {
            var query = _ratingsContext.Marks
                        .Where(c => c.WorkId == workId)
                        .GroupBy(c => c.WorkId)
                        .Select(c => new MarkResponse
                        {
                            WorkId = c.Key,
                            Rating = c.Average(d => d.MarkValue),
                            MarkCount = c.Count()
                        });

            var result = await query.SingleOrDefaultAsync();

            if (userId > 0)
            {
                var userMark = await _ratingsContext.Marks
                    .Where(c => c.WorkId == workId && c.UserId == userId)
                    .Select(c => c.MarkValue)
                    .SingleOrDefaultAsync();

                if (userMark > 0)
                {
                    result.UserMark = userMark;
                }
            }

            return result;
        }
    }
}
