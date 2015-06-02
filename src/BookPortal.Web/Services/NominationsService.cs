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

        public async Task<Nomination> AddNominationAsync(Nomination request)
        {
            _bookContext.Add(request);
            await _bookContext.SaveChangesAsync();

            return request;
        }

        public async Task<Nomination> UpdateNominationAsync(int id, Nomination request)
        {
            Nomination nomination = await _bookContext.Nominations.Where(a => a.Id == id).SingleOrDefaultAsync();

            if (nomination == null)
                return await Task.FromResult(default(Nomination));

            _bookContext.Update(request);
            await _bookContext.SaveChangesAsync();

            return request;
        }

        public async Task<Nomination> DeleteNominationAsync(int id)
        {
            Nomination nomination = await _bookContext.Nominations.Where(a => a.Id == id).SingleOrDefaultAsync();

            if (nomination != null)
            {
                _bookContext.Remove(nomination);
                await _bookContext.SaveChangesAsync();
            }

            return nomination;
        }
    }
}
