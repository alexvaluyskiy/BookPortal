using System;
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

        public virtual async Task<IReadOnlyList<Work>> GetWorksAsync()
        {
            return await _bookContext.Works.ToListAsync();
        }

        public virtual async Task<Work> GetWorkAsync(int workId)
        {
            return await _bookContext.Works.Where(c => c.Id == workId).SingleOrDefaultAsync();
        }
    }
}
