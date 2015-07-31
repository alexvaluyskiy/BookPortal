using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services
{
    public class ContestsWorksService
    {
        private readonly BookContext _bookContext;

        public ContestsWorksService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<ApiObject<ContestWorkResponse>> GetContestsWorksAsync(int contestId)
        {
            var query = _bookContext.ContestWorks
                .Where(c => c.ContestId == contestId)
                .Select(c => new ContestWorkResponse
                {
                    ContestWorkId = c.Id,
                    Name = c.Name,
                    RusName = c.RusName,
                    Prefix = c.Prefix,
                    Postfix = c.Postfix,
                    Number = c.Number,
                    IsWinner = c.IsWinner,
                    LinkType = c.LinkType,
                    LinkId = c.LinkId,
                    ContestId = c.ContestId,
                    NominationId = c.NominationId
                });

            var result = new ApiObject<ContestWorkResponse>();
            result.Values = await query.ToListAsync();
            result.TotalRows = result.Values.Count;

            return result;
        }

        public async Task<ContestWorkResponse> GetContestWorkAsync(int contestId, int contestWorkId)
        {
            var query = _bookContext.ContestWorks
                .Where(c => c.Id == contestWorkId)
                .Select(c => new ContestWorkResponse
                {
                    ContestWorkId = c.Id,
                    Name = c.Name,
                    RusName = c.RusName,
                    Prefix = c.Prefix,
                    Postfix = c.Postfix,
                    Number = c.Number,
                    IsWinner = c.IsWinner,
                    LinkType = c.LinkType,
                    LinkId = c.LinkId,
                    ContestId = c.ContestId,
                    NominationId = c.NominationId
                });

            return await query.SingleOrDefaultAsync();
        }
    }
}