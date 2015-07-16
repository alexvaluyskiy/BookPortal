using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class PublishersService
    {
        private readonly BookContext _bookContext;

        public PublishersService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<PublisherResponse> GetPublisherAsync(int publisherId)
        {
            var query = from p in _bookContext.Publishers
                        join c in _bookContext.Countries on p.CountryId equals c.Id
                        where p.Id == publisherId
                        select new PublisherResponse
                        {
                            PublisherId = p.Id,
                            Name = p.Name,
                            Type = p.Type,
                            YearOpen = p.YearOpen,
                            YearClose = p.YearClose,
                            Description = p.Description,
                            DescriptionSource = p.DescriptionSource,
                            CountryId = c.Id,
                            CountryName = c.Name
                        };

            return await query.SingleOrDefaultAsync();
        }

        // TODO: add paging
        public async Task<IReadOnlyList<EditionResponse>> GetPublisherEditionsAsync(int publisherId)
        {
            var query = from e in _bookContext.Editions
                        join ep in _bookContext.EditionPublishers on e.Id equals ep.EditionId
                        where ep.PublisherId == publisherId
                        select new EditionResponse
                        {
                            EditionId = e.Id,
                            Name = e.Name,
                            Year = e.Year,
                            Correct = 1
                        };

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<SerieResponse>> GetPublisherSeriesAsync(int publisherId)
        {
            // TODO: EF7 bug: No inner join
            var series2 = _bookContext.PublisherSeries
                .Include(c => c.Serie)
                .Where(c => c.PublisherId == publisherId)
                .Select(c => c.Serie);

            var query = from ps in _bookContext.PublisherSeries
                        join s in _bookContext.Series on ps.SerieId equals s.Id
                        where ps.PublisherId == publisherId
                        orderby s.SerieClosed
                        select new SerieResponse
                        {
                            SerieId = s.Id,
                            Name = s.Name,
                            Description = s.Description,
                            YearOpen = s.YearOpen,
                            YearClose = s.YearClose,
                            SerieClosed = s.SerieClosed
                        };

            var series = await query.ToListAsync();

            return series;
        }

        public async Task<IReadOnlyList<AwardItemResponse>> GetPublisherAwardsAsync(int publisherId)
        {
            var query = from cw in _bookContext.ContestWorks
                        join c in _bookContext.Contests on cw.ContestId equals c.Id
                        join n in _bookContext.Nominations on cw.NominationId equals n.Id
                        join a in _bookContext.Awards on c.AwardId equals a.Id
                        where cw.LinkType == ContestWorkType.Publisher && cw.LinkId == publisherId
                        select new AwardItemResponse
                        {
                            AwardId = a.Id,
                            AwardRusname = a.RusName,
                            AwardName = a.Name,
                            AwardIsOpened = a.IsOpened,
                            ContestId = c.Id,
                            ContestName = c.Name,
                            ContestYear = c.NameYear,
                            NominationId = n.Id,
                            NominationRusname = n.RusName,
                            NominationName = n.Name,
                            ContestWorkId = cw.Id,
                            ContestWorkRusname = cw.RusName,
                            ContestWorkName = cw.Name,
                            ContestWorkPrefix = cw.Prefix,
                            ContestWorkPostfix = cw.Postfix
                        };

            return await query.ToListAsync();
        }
    }
}
