using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class NominationsService
    {
        private readonly BookContext _bookContext;

        public NominationsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<Nomination>> GetNominationsAsync(int awardId)
        {
            return await _bookContext.Nominations.Where(n => n.AwardId == awardId).ToListAsync();
        }

        public Task<Nomination> GetNominationAsync(int awardId, int nominationId)
        {
            return _bookContext.Nominations.Where(n => n.AwardId == awardId && n.Id == nominationId).SingleOrDefaultAsync();
        }
    }
}
