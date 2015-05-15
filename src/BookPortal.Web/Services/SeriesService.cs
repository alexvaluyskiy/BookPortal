using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class SeriesService
    {
        private readonly BookContext _bookContext;

        public SeriesService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<Serie>> GetSeriesAsync(int publisherId)
        {
            return await _bookContext.Series.Where(c => c.PublisherId == publisherId).ToListAsync();
        }

        public async Task<Serie> GetSerieAsync(int id)
        {
            return await _bookContext.Series.Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}
