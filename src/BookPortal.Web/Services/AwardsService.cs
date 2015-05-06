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

        public virtual async Task<IReadOnlyCollection<Award>> GetAwardsAsync(AwardRequest request)
        {
            var result = _bookContext.Awards.AsQueryable();

            if (request.IncludeNominations)
                result = result.Include(c => c.Nominations);

            if (request.IncludeContests)
                result = result.Include(c => c.Contests);

            if (request.Offset.HasValue && request.Offset.Value > 0)
                result = result.Skip(request.Offset.Value);

            if (request.Limit.HasValue && request.Limit > 0)
                result = result.Take(request.Limit.Value);

            return await result.ToListAsync();
        }

        public virtual async Task<Award> GetAwardAsync(int awardId)
        {
            return await _bookContext.Awards.Where(a => a.Id == awardId).SingleOrDefaultAsync();
        }

        public virtual async Task<Award> AddAwardAsync(Award request)
        {
            _bookContext.Add(request);
            await _bookContext.SaveChangesAsync();

            return request;
        }

        public virtual async Task<Award> UpdateAwardAsync(int awardId, Award request)
        {
            Award award = await _bookContext.Awards.Where(a => a.Id == awardId).SingleOrDefaultAsync();

            if (award == null)
                return await Task.FromResult(default(Award));

            _bookContext.Update(request);
            await _bookContext.SaveChangesAsync();

            return request;
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
