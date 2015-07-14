using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class ContestsWorksService
    {
        private readonly BookContext _bookContext;

        public ContestsWorksService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<ContestWork>> GetContestsWorksAsync(int contestId)
        {
            return await _bookContext.ContestWorks.Where(c => c.ContestId == contestId).ToListAsync();
        }

        public Task<ContestWork> GetContestWorkAsync(int contestId, int contestWorkId)
        {
            return _bookContext.ContestWorks.Where(c => c.Id == contestWorkId).SingleOrDefaultAsync();
        }
    }
}