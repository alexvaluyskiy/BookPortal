using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services
{
    public class NominationsService
    {
        private readonly BookContext _bookContext;

        public NominationsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<ApiObject<NominationResponse>> GetNominationsAsync(int awardId)
        {
            var query = _bookContext.Nominations
                .Where(n => n.AwardId == awardId)
                .Select(c => new NominationResponse
                {
                    NominationId = c.Id,
                    Name = c.Name,
                    RusName = c.RusName,
                    Description = c.Description,
                    Number = c.Number,
                    AwardId = c.AwardId
                });

            return new ApiObject<NominationResponse>(await query.ToListAsync());
        }

        public async Task<NominationResponse> GetNominationAsync(int awardId, int nominationId)
        {
            var query = _bookContext.Nominations
                .Where(n => n.AwardId == awardId && n.Id == nominationId)
                .Select(c => new NominationResponse
                {
                    NominationId = c.Id,
                    Name = c.Name,
                    RusName = c.RusName,
                    Description = c.Description,
                    Number = c.Number,
                    AwardId = c.AwardId
                });

            return await query.SingleOrDefaultAsync();
        }
    }
}
