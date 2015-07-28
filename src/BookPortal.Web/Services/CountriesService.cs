using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class CountriesService
    {
        private readonly BookContext _bookContext;

        public CountriesService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<Country>> GetCountriesAsync()
        {
            return await _bookContext.Countries.ToListAsync();
        }

        public Task<Country> GetCountryAsync(int id)
        {
            return _bookContext.Countries.Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}
