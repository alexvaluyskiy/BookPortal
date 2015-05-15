using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return await _bookContext.Editions.Where(c => c.WorkId == workId).ToListAsync();
        }

        public async Task<Edition> GetEditionAsync(int editionId)
        {
            return await _bookContext.Editions.Where(c => c.Id == editionId).SingleOrDefaultAsync();
        }

    }
}
