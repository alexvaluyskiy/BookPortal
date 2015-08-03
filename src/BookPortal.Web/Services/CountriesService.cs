using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Responses;
using Microsoft.Framework.Caching.Distributed;
using Microsoft.Framework.Caching.Redis;
using Newtonsoft.Json;

namespace BookPortal.Web.Services
{
    public class CountriesService
    {
        private readonly BookContext _bookContext;
        private readonly RedisCache _cache;

        public CountriesService(BookContext bookContext, RedisCache cache)
        {
            _bookContext = bookContext;
            _cache = cache;
        }

        public async Task<ApiObject<CountryResponse>> GetCountriesAsync()
        {
            var cacheEntryBytes = await _cache.GetAsync("countries");

            List<CountryResponse> countryResponses;
            if (cacheEntryBytes == null)
            {
                var query = _bookContext.Countries
                            .Select(c => new CountryResponse
                            {
                                CountryId = c.Id,
                                Name = c.Name
                            });

                countryResponses = await query.ToListAsync();

                string serialized = JsonConvert.SerializeObject(countryResponses);
                await _cache.SetAsync("countries", Encoding.UTF8.GetBytes(serialized));
            }
            else
            {
                var cachedString = Encoding.UTF8.GetString(cacheEntryBytes);
                countryResponses = JsonConvert.DeserializeObject<List<CountryResponse>>(cachedString);
            }

            return new ApiObject<CountryResponse>(countryResponses);
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
