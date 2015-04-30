using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class AwardsService
    {
        private readonly BookContext _bookContext;

        public AwardsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public virtual async Task<IReadOnlyCollection<Award>> GetAwardsAsync()
        {
            return await _bookContext.Awards.ToListAsync();
        }

        public virtual async Task<Award> GetAwardAsync(int awardId)
        {
            return await _bookContext.Awards.Where(a => a.Id == awardId).SingleOrDefaultAsync();
        }

        public virtual async Task<Award> AddAwardAsync(AwardRequest request)
        {
            Award award = new Award()
            {
                Name = request.Name
            };

            _bookContext.Add(award);
            await _bookContext.SaveChangesAsync();

            return award;
        }

        public virtual async Task<Award> UpdateAwardAsync(int awardId, AwardRequest request)
        {
            Award award = await _bookContext.Awards.Where(a => a.Id == awardId).SingleOrDefaultAsync();

            award.Name = request.Name;

            _bookContext.Update(award);
            await _bookContext.SaveChangesAsync();

            return award;
        }

        public virtual async Task<Award> DeleteAwardAsync(int awardId)
        {
            Award award = await _bookContext.Awards.Where(a => a.Id == awardId).SingleOrDefaultAsync();

            if (award != null)
            {
                _bookContext.Remove(award);
                await _bookContext.SaveChangesAsync();
            }

            return award;
        }
    }
}
