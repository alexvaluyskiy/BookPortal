using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class EditionsService
    {
        private readonly BookContext _bookContext;

        public EditionsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<Edition>> GetEditionsAsync(int workId)
        {
            // TODO: workaround for EF7 bug, which haven't supported selectmany yet
            var editionIds = _bookContext.EditionWorks.Where(c => c.WorkId == workId).Select(c => c.EditionId).ToList();

            return await _bookContext.Editions
                .Where(c => editionIds.Contains(c.Id))
                .ToListAsync();
        }

        public async Task<Edition> GetEditionAsync(int editionId)
        {
            return await _bookContext.Editions.Where(c => c.Id == editionId).SingleOrDefaultAsync();
        }

    }
}
