using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public virtual Task<List<Country>> GetCountriesAsync()
        {
            return _bookContext.Countries.ToListAsync();
        }

        public virtual Task<Country> GetCountryAsync(int id)
        {
            return _bookContext.Countries.Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}
