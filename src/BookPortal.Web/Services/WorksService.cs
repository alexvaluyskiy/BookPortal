using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class WorksService
    {
        private readonly BookContext _bookContext;

        public WorksService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public virtual async Task<IReadOnlyList<WorkResponse>> GetWorksAsync(int personId)
        {
            var workIds = _bookContext.PersonWorks.Where(c => c.PersonId == personId).Select(c => c.WorkId).ToList();

            // TODO: add recursive search
            var workLinks = _bookContext.WorkLinks.Where(c => workIds.Contains(c.WorkId)).ToList();
            var workLinksIds = workLinks.Select(c => c.WorkId).ToList();

            workLinks.Add( new WorkLink() { WorkId = 1, ParentWorkId = 2 });

            var works = await (from w in _bookContext.Works
                        join wt in _bookContext.WorkTypes on w.WorkTypeId equals wt.Id
                        where workLinksIds.Contains(w.Id)
                        select new WorkResponse
                        {
                            WorkId = w.Id,
                            RusName = w.RusName,
                            Name = w.Name,
                            AltName = w.AltName,
                            Year = w.Year,
                            Description = w.Description,
                            WorkTypeId = wt.Id,
                            WorkTypeName = wt.Name,
                            WorkTypeLevel = wt.Level
                        }).ToListAsync();

            foreach (var work in works)
            {
                var childWorks = workLinks.Where(c => c.ParentWorkId == work.WorkId).ToList();
                if (childWorks.Count > 0)
                {

                }
            }

            return works;
        }

        public virtual async Task<Work> GetWorkAsync(int workId)
        {
            return await _bookContext.Works
                .Include(c => c.WorkType)
                .Where(c => c.Id == workId)
                .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<ContestWorkResponse>> GetWorkAwardsAsync(int workId)
        {
            var query = from cw in _bookContext.ContestWorks
                        join c in _bookContext.Contests on cw.ContestId equals c.Id
                        join n in _bookContext.Nominations on cw.NominationId equals n.Id
                        join a in _bookContext.Awards on c.AwardId equals a.Id
                        where cw.LinkType == ContestWorkType.Work && cw.LinkId == workId
                        select new ContestWorkResponse
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

        public async Task<IReadOnlyList<EditionResponse>> GetWorkEditionsAsync(int workId)
        {
            var query = from e in _bookContext.Editions
                        join ew in _bookContext.EditionWorks on e.Id equals ew.EditionId
                        where ew.WorkId == workId
                        select new EditionResponse
                        {
                            EditionId = e.Id,
                            Name = e.Name,
                            Year = e.Year,
                            Correct = 1
                        };

            return await query.ToListAsync();
        }
    }
}
