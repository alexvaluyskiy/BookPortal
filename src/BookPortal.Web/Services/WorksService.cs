using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class WorksService
    {
        private readonly BookContext _bookContext;

        public WorksService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public virtual async Task<IReadOnlyList<Work>> GetWorksAsync(int personId)
        {
            // TODO: workaround for EF7 bug, which haven't supported selectmany yet
            var workIds = _bookContext.PersonWorks.Where(c => c.PersonId == personId).Select(c => c.WorkId).ToList();

            var query = from c in _bookContext.Works
                        where workIds.Contains(c.Id)
                        select c;

            return await query.ToListAsync();
        }

        public virtual async Task<Work> GetWorkAsync(int workId)
        {
            return await _bookContext.Works
                .Include(c => c.WorkType)
                .Where(c => c.Id == workId)
                .SingleOrDefaultAsync();
        }
    }
}
