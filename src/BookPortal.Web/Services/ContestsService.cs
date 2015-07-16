using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class ContestsService
    {
        private readonly BookContext _bookContext;

        public ContestsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<ContestResponse>> GetContestsAsync(int awardId)
        {
            var query = _bookContext.Contests
                .Where(c => c.AwardId == awardId)
                .Select(c => new ContestResponse
                {
                    ContestId = c.Id,
                    Name = c.Name,
                    NameYear = c.NameYear,
                    Number = c.Number,
                    Place = c.Place,
                    Date = c.Date,
                    Description = c.Description,
                    ShortDescription = c.ShortDescription,
                    AwardId = c.AwardId
                });

            return await query.ToListAsync();
        }

        public async Task<ContestResponse> GetContestAsync(int awardId, int contestId)
        {
            var query = _bookContext.Contests
                .Where(c => c.AwardId == awardId && c.Id == contestId)
                .Select(c => new ContestResponse
                {
                    ContestId = c.Id,
                    Name = c.Name,
                    NameYear = c.NameYear,
                    Number = c.Number,
                    Place = c.Place,
                    Date = c.Date,
                    Description = c.Description,
                    ShortDescription = c.ShortDescription,
                    AwardId = c.AwardId
                });

            return await query.SingleOrDefaultAsync();
        }
    }
}