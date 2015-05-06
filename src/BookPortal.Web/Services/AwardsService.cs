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

            result = result.Include(c => c.Country);
            result = result.Include(c => c.Language);

            result = result.OrderBy(c => c.RusName);

            if (request.Offset.HasValue && request.Offset.Value > 0)
                result = result.Skip(request.Offset.Value);

            if (request.Limit.HasValue && request.Limit > 0)
                result = result.Take(request.Limit.Value);

            IReadOnlyCollection<Award> awards = await result.ToListAsync();

            // TODO: workaround for EF7 beta 4 bug
            if (request.IncludeNominations || request.IncludeContests)
            {
                foreach (var award in awards)
                {
                    await PopulateChildren(request, award);
                }
            }

            return awards;
        }

        public virtual async Task<Award> GetAwardAsync(int awardId)
        {
            var result = _bookContext.Awards.AsQueryable();

            result = result.Include(c => c.Country);
            result = result.Include(c => c.Language);

            result = result.Where(a => a.Id == awardId);

            return await result.SingleOrDefaultAsync();
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

        private async Task PopulateChildren(AwardRequest request, Award award)
        {
            if (request.IncludeNominations)
            {
                var nominations = await _bookContext.Nominations
                    .Where(n => n.AwardId == award.Id)
                    .ToListAsync();

                foreach (var nomination in nominations)
                {
                    nomination.ContestWorks = null;
                    award.Nominations.Add(nomination);
                }
            }

            if (request.IncludeContests)
            {
                var contests = await _bookContext.Contests
                    .Where(c => c.AwardId == award.Id)
                    .ToListAsync();

                foreach (var contest in contests)
                {
                    if (request.IncludeContestsWorks)
                    {
                        var contestWorksQuery = _bookContext.ContestWorks.Where(c => c.ContestId == contest.Id);

                        if (request.IsWinnersOnly)
                            contestWorksQuery = contestWorksQuery.Where(c => c.IsWinner);

                        var contestWorks = await contestWorksQuery.ToListAsync();

                        foreach (var contestWork in contestWorks)
                        {
                            contestWork.Nomination = null;
                            contest.ContestWorks.Add(contestWork);
                        }
                    }

                    award.Contests.Add(contest);
                }
            }
        }
    }
}
