using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
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
                            DescriptionSource = a.DescriptionSource,
                            Notes = a.Notes,
                            AwardClosed = a.AwardClosed,
                            IsOpened = a.IsOpened,
                            LanguageId = l.Id,
                            LanguageName = l.Name,
                            CountryId = c.Id,
                            CountryName = c.Name
                        };

            if (request.IsOpened)
                query = query.Where(a => a.IsOpened);

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

            if (request.Offset > 0)
                query = query.Skip(request.Offset);

            if (request.Limit > 0)
                query = query.Take(request.Limit);

            var queryResults = await query.ToListAsync();

            // TODO: Min/Max is not translating into SQL
            var awardsIds = queryResults.Select(c => c.Id).ToList();
            var awardFirstLastContests = (  from c in _bookContext.Contests
                                            where awardsIds.Contains(c.AwardId)
                                            group c by c.AwardId into g
                                            select new
                                            {
                                                AwardId = g.Key,
                                                FirstContestDate = g.Min(d => d.Date),
                                                LastContestDate = g.Max(d => d.Date)
                                            }).ToList();

            foreach (var queryResult in queryResults)
            {
                queryResult.FirstContestDate = awardFirstLastContests
                    .Where(c => c.AwardId == queryResult.Id)
                    .Select(c => c.FirstContestDate)
                    .SingleOrDefault();
                queryResult.LastContestDate = awardFirstLastContests
                    .Where(c => c.AwardId == queryResult.Id)
                    .Select(c => c.LastContestDate)
                    .SingleOrDefault();
            }

            return queryResults;
        }

        public virtual async Task<int> GetAwardCountsAsync(AwardRequest request)
        {
            var query = _bookContext.Awards.AsQueryable();

            if (request.IsOpened)
                query = query.Where(a => a.IsOpened);

            return await query.CountAsync();
        }

        public virtual async Task<AwardResponse> GetAwardAsync(int awardId)
        {
            var query = from a in _bookContext.Awards
                        join l in _bookContext.Languages on a.LanguageId equals l.Id
                        join c in _bookContext.Countries on a.CountryId equals c.Id
                        where a.Id == awardId
                        select new AwardResponse
                        {
                            Id = a.Id,
                            RusName = a.RusName,
                            Name = a.Name,
                            Homepage = a.Homepage,
                            Description = a.Description,
                            DescriptionSource = a.DescriptionSource,
                            Notes = a.Notes,
                            AwardClosed = a.AwardClosed,
                            IsOpened = a.IsOpened,
                            LanguageId = l.Id,
                            LanguageName = l.Name,
                            CountryId = c.Id,
                            CountryName = c.Name,
                            FirstContestDate = _bookContext.Contests.Where(f => f.AwardId == awardId).Min(f => f.Date),
                            LastContestDate = _bookContext.Contests.Where(f => f.AwardId == awardId).Max(f => f.Date)
                        };

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<AwardResponse> AddAwardAsync(Award request)
        {
            var added = _bookContext.Add(request);
            await _bookContext.SaveChangesAsync();

            return Mapper.Map<AwardResponse>(added.Entity);
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
