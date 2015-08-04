using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services.Internal
{
    public class CronJobs
    {
        private readonly BookContext _bookContext;

        public CronJobs(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task UpdateGenreViews(int workId)
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
            var personIds = _bookContext.PersonWorks.Where(c => c.WorkId == workId).Select(c => c.PersonId).ToList();

            foreach (var personId in personIds)
            {
                var genrePersonView = (from gpv in _bookContext.GenrePersonsView
                                       where gpv.PersonId == personId
                                       select gpv).ToDictionary(c => c.GenreWorkId, c => c);

                // TODO: combine when EF7 will support GroupBy
                var genrePersonCount = (from gwv in _bookContext.GenreWorksView
                                        join gw in _bookContext.GenreWorks on gwv.GenreWorkId equals gw.Id
                                        join pw in _bookContext.PersonWorks on gwv.WorkId equals pw.WorkId
                                        where pw.PersonId == personId && gw.GenreWorkGroupId == 1 && gw.ParentGenreWorkId == null
                                        select gwv).ToList();

                var genrePersonCountGroup = genrePersonCount
                    .GroupBy(c => c.GenreWorkId)
                    .Select(g => new
                    {
                        GenreWorkId = g.Key,
                        Count = g.Sum(c => c.GenreCount)
                    }).ToList();

                foreach (var genrePerson in genrePersonCountGroup)
                {
                    GenrePersonView genrePersonViewOut;
                    genrePersonView.TryGetValue(genrePerson.GenreWorkId, out genrePersonViewOut);

                    // if number changed
                    if (genrePersonViewOut != null)
                    {
                        if (genrePersonViewOut.GenreCount != genrePerson.Count)
                        {
                            genrePersonViewOut.GenreCount = genrePerson.Count;
                            _bookContext.Update(genrePersonViewOut);
                        }
                    }
                    // if no value in the view
                    else
                    {
                        genrePersonViewOut = new GenrePersonView();
                        genrePersonViewOut.PersonId = personId;
                        genrePersonViewOut.GenreWorkId = genrePerson.GenreWorkId;
                        genrePersonViewOut.GenreCount = genrePerson.Count;

                        _bookContext.Add(genrePersonViewOut);
                    }
                }
            }

            await _bookContext.SaveChangesAsync();
        }
    }
}
