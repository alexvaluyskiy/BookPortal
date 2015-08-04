using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Microsoft.Framework.OptionsModel;

namespace BookPortal.Web.Services
{
    public class GenresService
    {
        private readonly BookContext _bookContext;
        private readonly IOptions<AppSettings> _options;

        public GenresService(BookContext bookContext, IOptions<AppSettings> options)
        {
            _bookContext = bookContext;
            _options = options;
        }

        public async Task<ApiObject<GenreWorkResponse>> GetAuthorGenres(int personId)
        {
            var query = from gpv in _bookContext.GenrePersonsView
                        join gw in _bookContext.GenreWorks on gpv.GenreWorkId equals gw.Id
                        where gpv.PersonId == personId
                        select new GenreWorkResponse
                        {
                            GenreWorkId = gw.Id,
                            Name = gw.Name,
                            GenreCount = gpv.GenreCount,
                            GenreTotal = gpv.GenreTotal
                        };

            query = query.OrderByDescending(c => c.GenreCount);

            if (_options.Options.PersonGenreLimit > 0)
                query = query.Take(_options.Options.PersonGenreLimit);

            var result = await query.ToListAsync();

            return new ApiObject<GenreWorkResponse>(result);
        }

        public async Task<ApiObject<GenrePersonResponse>> GetWorkGenres(int workId)
        {
            var genre = await (from gwv in _bookContext.GenreWorksView
                              join gw in _bookContext.GenreWorks on gwv.GenreWorkId equals gw.Id
                              where gwv.WorkId == workId
                              select new GenrePersonResponse
                              {
                                  GenreWorkId = gw.Id,
                                  GenreParentWorkId = gw.ParentGenreWorkId,
                                  Name = gw.Name,
                                  Level = gw.Level,
                                  GenreCount = gwv.GenreCount,
                                  GenreWorkGroupId = gw.GenreWorkGroupId
                              }).ToListAsync();

            return new ApiObject<GenrePersonResponse>(genre);
        }


    }
}
