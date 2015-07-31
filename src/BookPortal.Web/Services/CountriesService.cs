using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services
{
    public class CountriesService
    {
        private readonly BookContext _bookContext;

        public CountriesService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<ApiObject<CountryResponse>> GetCountriesAsync()
        {
            var query = _bookContext.Countries
                .Select(c => new CountryResponse
                {
                    CountryId = c.Id,
                    Name = c.Name
                });

            var result = new ApiObject<CountryResponse>();
            result.Values = await query.ToListAsync();
            result.TotalRows = result.Values.Count;

            return result;
        }

        public Task<CountryResponse> GetCountryAsync(int id)
        {
            var query = _bookContext.Languages
                .Where(c => c.Id == id)
                .Select(c => new CountryResponse
                {
                    CountryId = c.Id,
                    Name = c.Name
                });

            return query.SingleOrDefaultAsync();
        }
    }
}
