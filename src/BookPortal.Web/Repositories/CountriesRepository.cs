using System;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Dapper;
using Microsoft.Framework.Caching.Memory;

namespace BookPortal.Web.Repositories
{
    public class CountriesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IMemoryCache _memoryCache;

        public CountriesRepository(IConnectionFactory connectionFactory, IMemoryCache memoryCache)
        {
            _connectionFactory = connectionFactory;
            _memoryCache = memoryCache;
        }

        public async Task<ApiObject<CountryResponse>> GetCountriesAsync()
        {
            ApiObject<CountryResponse> value;
            string cacheKey = "countries";

            if (!_memoryCache.TryGetValue(cacheKey, out value))
            {
                using (var connection = _connectionFactory.Create())
                {
                    var sql = "SELECT country_id as 'CountryId', name FROM countries ORDER BY name";
                    var countries = await connection.QueryAsync<CountryResponse>(sql);

                    value = new ApiObject<CountryResponse>(countries);
                    _memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
                }
            }

            return value;
        }

        public async Task<CountryResponse> GetCountryAsync(int countryId)
        {
            CountryResponse value;
            string cacheKey = $"countries:{countryId}";

            if (!_memoryCache.TryGetValue(cacheKey, out value))
            {
                using (var connection = _connectionFactory.Create())
                {
                    var sql = "SELECT country_id as 'CountryId', name FROM countries WHERE country_id = @countryId";
                    var countries = await connection.QueryAsync<CountryResponse>(sql, new { countryId });

                    value = countries.SingleOrDefault();
                    _memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
                }
            }

            return value;
        }
    }
}
