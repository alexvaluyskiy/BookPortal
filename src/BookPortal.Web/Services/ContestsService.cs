using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class ContestsService
    {
        private readonly BookContext _bookContext;

        public ContestsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<Contest>> GetContestsAsync(int awardId)
        {
            return await _bookContext.Contests.Where(c => c.AwardId == awardId).ToListAsync();
        }

        public Task<Contest> GetContestAsync(int awardId, int contestId)
        {
            return _bookContext.Contests.Where(c => c.AwardId == awardId && c.Id == contestId).SingleOrDefaultAsync();
        }
    }
}