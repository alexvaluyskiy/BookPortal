using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<List<ContestWork>> GetContestsWorksAsync(int contestId)
        {
            return _bookContext.ContestsWorks.Where(c => c.ContestId == contestId).ToListAsync();
        }

        public Task<ContestWork> GetContestWorkAsync(int contestId, int contestWorkId)
        {
            return _bookContext.ContestsWorks.Where(c => c.Id == contestWorkId).SingleOrDefaultAsync();
        }

        public Task<ContestWork> AddContestWorkAsync(ContestWork request)
        {
            throw new NotImplementedException();
        }

        public Task<ContestWork> UpdateContestWorkAsync(int contestWorkId, ContestWork request)
        {
            throw new NotImplementedException();
        }

        public Task<ContestWork> DeleteContestWorkAsync(int contestWorkId)
        {
            throw new NotImplementedException();
        }
    }
}