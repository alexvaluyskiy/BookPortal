using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services
{
    public class EditionsService
    {
        private readonly BookContext _bookContext;

        public EditionsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<EditionResponse> GetEditionAsync(int editionId)
        {
            var query = _bookContext.Editions
                .Where(c => c.Id == editionId)
                .Select(c => new EditionResponse
                {
                    EditionId = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Authors = c.Authors,
                    Compilers = c.Compilers,
                    Isbn = c.Isbn,
                    Year = c.Year,
                    ReleaseDate = c.ReleaseDate,
                    Count = c.Count,
                    CoverType = c.CoverType,
                    SuperCover = c.SuperCover,
                    Format = c.Format,
                    Pages = c.Pages,
                    Description = c.Description,
                    Content = c.Content,
                    Notes = c.Notes,
                    LanguageId = c.LanguageId
                });

            return await query.SingleOrDefaultAsync();
        }

        public async Task<ApiObject<EditionResponse>> GetEditionsByPersonAsync(int personId)
        {
            var editions = from e in _bookContext.Editions
                           join ew in _bookContext.EditionWorks on e.Id equals ew.EditionId
                           join w in _bookContext.PersonWorks on ew.WorkId equals w.WorkId
                           where w.PersonId == personId
                           select new EditionResponse
                           {
                               EditionId = e.Id,
                               Name = e.Name,
                               Year = e.Year,
                               Correct = 1
                           };

            return new ApiObject<EditionResponse>(await editions.ToListAsync());
        }

        // TODO: for works page create simplier versions of this API (last five translated)
        // TODO: created extended versions with grouping and sorting
        public async Task<ApiObject<EditionResponse>> GetEditionsByWorkAsync(int workId)
        {
            var editions = from e in _bookContext.Editions
                           join ew in _bookContext.EditionWorks on e.Id equals ew.EditionId
                           where ew.WorkId == workId
                           select new EditionResponse
                           {
                               EditionId = e.Id,
                               Name = e.Name,
                               Year = e.Year,
                               Correct = 1
                           };

            return new ApiObject<EditionResponse>(await editions.ToListAsync());
        }

        // TODO: add paging
        public async Task<ApiObject<EditionResponse>> GetEditionsByPublisherAsync(int publisherId)
        {
            var editions = from e in _bookContext.Editions
                           join ep in _bookContext.EditionPublishers on e.Id equals ep.EditionId
                           where ep.PublisherId == publisherId
                           select new EditionResponse
                           {
                               EditionId = e.Id,
                               Name = e.Name,
                               Year = e.Year,
                               Correct = 1
                           };

            return new ApiObject<EditionResponse>(await editions.ToListAsync());
        }

        // TODO: add Correct field to editions
        // TODO: fix authors sorting
        public async Task<ApiObject<EditionResponse>> GetEditionsBySerieAsync(SerieRequest request)
        {
            var query = from e in _bookContext.Editions
                        join es in _bookContext.EditionSeries on e.Id equals es.EditionId
                        where es.SerieId == request.SerieId
                        select new EditionResponse
                        {
                            EditionId = e.Id,
                            Name = e.Name,
                            Year = e.Year,
                            Correct = 1,
                            SerieSort = es.Sort
                        };

            switch (request.Sort)
            {
                case EditionsSort.Name:
                    query = query.OrderBy(c => c.Name).ThenBy(c => c.Year); ;
                    break;
                case EditionsSort.Authors:
                    query = query.OrderBy(c => c.Authors).ThenBy(c => c.SerieSort).ThenBy(c => c.Year);
                    break;
                case EditionsSort.Year:
                    query = query.OrderBy(c => c.Year).ThenBy(c => c.SerieSort).ThenBy(c => c.Name);
                    break;
                default:
                    query = query.OrderByDescending(c => c.Year).ThenBy(c => c.SerieSort);
                    break;
            }

            if (request.Offset > 0)
                query = query.Take(request.Offset);

            if (request.Limit > 0)
                query = query.Take(request.Limit);

            var totalRows = await _bookContext.EditionSeries.CountAsync(c => c.SerieId == request.SerieId);
            var result = new ApiObject<EditionResponse>(await query.ToListAsync(), totalRows);

            return result;
        }
    }
}
