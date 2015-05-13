using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public virtual async Task<IReadOnlyList<AwardResponse>> GetAwardsAsync(AwardRequest request)
        {
            var query = from a in _bookContext.Awards
                        join l in _bookContext.Languages on a.LanguageId equals l.Id
                        join c in _bookContext.Countries on a.CountryId equals c.Id
                        select new AwardResponse
                        {
                            Id = a.Id,
                            RusName = a.RusName,
                            Name = a.Name,
                            Homepage = a.Homepage,
                            Description = a.Description,
                            DescriptionCopyright = a.DescriptionCopyright,
                            Notes = a.Notes,
                            AwardClosed = a.AwardClosed,
                            IsOpened = a.IsOpened,
                            LanguageId = l.Id,
                            LanguageName = l.Name,
                            CountryId = c.Id,
                            CountryName = c.Name,
                            FirstContestDate = _bookContext.Contests.Where(f => f.AwardId == a.Id).Min(f => f.Date),
                            LastContestDate = _bookContext.Contests.Where(f => f.AwardId == a.Id).Max(f => f.Date)
                        };

            if (request.IsOpened)
                query = query.Where(a => a.IsOpened);

            if (request.Offset.HasValue && request.Offset.Value > 0)
                query = query.Skip(request.Offset.Value);

            if (request.Limit.HasValue && request.Limit > 0)
                query = query.Take(request.Limit.Value);

            switch (request.Sort)
            {
                case AwardSort.Id:
                    query = query.OrderBy(c => c.Id).ThenBy(c => c.RusName); ;
                    break;
                case AwardSort.Rusname:
                    query = query.OrderBy(c => c.RusName);
                    break;
                case AwardSort.Language:
                    query = query.OrderBy(c => c.LanguageName).ThenBy(c => c.RusName);
                    break;
                case AwardSort.Country:
                    query = query.OrderBy(c => c.CountryName).ThenBy(c => c.RusName);
                    break;
            }

            return await query.ToListAsync();
        }

        public virtual async Task<AwardResponse> GetAwardAsync(int awardId)
        {
            var query = from a in _bookContext.Awards
                        where a.Id == awardId
                        join l in _bookContext.Languages on a.LanguageId equals l.Id
                        join c in _bookContext.Countries on a.CountryId equals c.Id
                        select new AwardResponse
                        {
                            Id = a.Id,
                            RusName = a.RusName,
                            Name = a.Name,
                            Homepage = a.Homepage,
                            Description = a.Description,
                            DescriptionCopyright = a.DescriptionCopyright,
                            Notes = a.Notes,
                            AwardClosed = a.AwardClosed,
                            IsOpened = a.IsOpened,
                            LanguageId = l.Id,
                            LanguageName = l.Name,
                            CountryId = c.Id,
                            CountryName = c.Name,
                            FirstContestDate = _bookContext.Contests.Min(f => f.Date),
                            LastContestDate = _bookContext.Contests.Max(f => f.Date)
                        };

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<AwardResponse> AddAwardAsync(Award request)
        {
            var added = _bookContext.Add(request);
            await _bookContext.SaveChangesAsync();


            var a = Mapper.Map<AwardResponse>(added.Entity);
            return a;
        }

        public virtual async Task<AwardResponse> UpdateAwardAsync(int awardId, Award request)
        {
            Award award = await _bookContext.Awards.Where(a => a.Id == awardId).SingleOrDefaultAsync();

            if (award != null)
            {
                _bookContext.Update(request);
                await _bookContext.SaveChangesAsync();

                return Mapper.Map<AwardResponse>(award);
            }

            return null;
        }

        public virtual async Task<AwardResponse> DeleteAwardAsync(int awardId)
        {
            Award award = await _bookContext.Awards.Where(a => a.Id == awardId).SingleOrDefaultAsync();

            if (award != null)
            {
                _bookContext.Remove(award);
                await _bookContext.SaveChangesAsync();

                return Mapper.Map<AwardResponse>(award);
            }

            return null;
        }
    }
}
