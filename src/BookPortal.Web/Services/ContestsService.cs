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

        public async Task<Contest> AddContestAsync(Contest request)
        {
            _bookContext.Add(request);
            await _bookContext.SaveChangesAsync();

            return request;
        }

        public async Task<Contest> UpdateContestAsync(int contestId, Contest request)
        {
            Contest contest = await _bookContext.Contests.Where(a => a.Id == contestId).SingleOrDefaultAsync();

            if (contest == null)
                return await Task.FromResult(default(Contest));

            _bookContext.Update(request);
            await _bookContext.SaveChangesAsync();

            return request;
        }

        public async Task<Contest> DeleteContestAsync(int contestId)
        {
            Contest contest = await _bookContext.Contests.Where(a => a.Id == contestId).SingleOrDefaultAsync();

            if (contest != null)
            {
                _bookContext.Remove(contest);
                await _bookContext.SaveChangesAsync();
            }

            return contest;
        }
    }
}