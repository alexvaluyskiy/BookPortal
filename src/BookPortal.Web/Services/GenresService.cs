using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;

namespace BookPortal.Web.Services
{
    public class GenresService
    {
        private readonly BookContext _bookContext;

        public GenresService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<ApiObject<object>> GetAuthorGenres(int personId)
        {
            var genre = await (from gpv in _bookContext.GenrePersonsView
                              join gw in _bookContext.GenreWorks on gpv.GenreWorkId equals gw.Id
                              where gpv.PersonId == personId
                              select new
                              {
                                  GenreWorkId = gw.Id,
                                  Name = gw.Name,
                                  GenreCount = gpv.GenreCount,
                                  GenreTotal = gpv.GenreTotal
                              }).ToListAsync();

            return new ApiObject<object>(genre);
        }

        public async Task<ApiObject<object>> GetWorkGenres(int workId)
        {
            var genre = await (from gwv in _bookContext.GenreWorksView
                              join gw in _bookContext.GenreWorks on gwv.GenreWorkId equals gw.Id
                              where gwv.WorkId == workId
                              select new
                              {
                                  GenreWorkId = gw.Id,
                                  GenreParentWorkId = gw.ParentGenreWorkId,
                                  Name = gw.Name,
                                  Level = gw.Level,
                                  GenreCount = gwv.GenreCount,
                                  GroupId = gw.GenreWorkGroupId
                              }).ToListAsync();

            return new ApiObject<object>(genre);
        }
    }
}
