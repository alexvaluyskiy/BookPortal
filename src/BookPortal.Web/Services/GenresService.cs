using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

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
            await UpdateGenreViews(workId);

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

        private async Task UpdateGenreViews(int workId)
        {
            // calculate work genres
            var genreWorkView = _bookContext.GenreWorksView
                .Where(c => c.WorkId == workId)
                .ToDictionary(c => c.GenreWorkId, c => c);

            var genreWorkCount = (from gwu in _bookContext.GenreWorkUsers
                                 where gwu.WorkId == workId
                                 group gwu by gwu.GenreWorkId into g
                                 select new
                                 {
                                     GenreWorkId = g.Key,
                                     Count = g.Count()
                                 }).ToList();

            foreach (var genreWork in genreWorkCount)
            {
                GenreWorkView genreWorkViewOut;
                genreWorkView.TryGetValue(genreWork.GenreWorkId, out genreWorkViewOut);
               
                // if number changed
                if (genreWorkViewOut != null)
                {
                    if (genreWorkViewOut.GenreCount != genreWork.Count)
                    {
                        genreWorkViewOut.GenreCount = genreWork.Count;
                        _bookContext.Update(genreWorkViewOut);
                    }
                }
                // if no value in the view
                else
                {
                    genreWorkViewOut = new GenreWorkView();
                    genreWorkViewOut.WorkId = workId;
                    genreWorkViewOut.GenreWorkId = genreWork.GenreWorkId;
                    genreWorkViewOut.GenreCount = genreWork.Count;

                    _bookContext.Add(genreWorkViewOut);
                }
            }

            await _bookContext.SaveChangesAsync();

            // calculate person genres
            var genrePersonView = (from gpv in _bookContext.GenrePersonsView
                                   join c in _bookContext.PersonWorks on gpv.PersonId equals c.PersonId
                                   where c.WorkId == workId
                                   select gpv).ToDictionary(c => c.GenreWorkId, c => c);

            var personId =_bookContext.PersonWorks.Where(c => c.WorkId == workId).Select(c => c.PersonId).FirstOrDefault();
            var workIds = _bookContext.PersonWorks.Where(c => c.PersonId == personId).Select(c => c.WorkId).ToList();

            var genrePersonCount = (from gwv in _bookContext.GenreWorksView
                                    join gw in _bookContext.GenreWorks on gwv.GenreWorkId equals gw.Id
                                    where workIds.Contains(gwv.WorkId) && gw.GenreWorkGroupId == 1 && gw.ParentGenreWorkId == null
                                    group gwv by gwv.GenreWorkId into g
                                    select new
                                    {
                                        GenreWorkId = g.Key,
                                        Count = g.Sum(c => c.GenreCount)
                                    }).ToList();
        }
    }
}
