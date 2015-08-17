using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Microsoft.Framework.Caching.Memory;
using Dapper;

namespace BookPortal.Web.Repositories
{
    public class LanguagesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IMemoryCache _memoryCache;

        public LanguagesRepository(IConnectionFactory connectionFactory, IMemoryCache memoryCache)
        {
            _connectionFactory = connectionFactory;
            _memoryCache = memoryCache;
        }

        public async Task<ApiObject<LanguageResponse>> GetLanguagesAsync()
        {
            ApiObject<LanguageResponse> value;
            string cacheKey = "languages";

            if (!_memoryCache.TryGetValue(cacheKey, out value))
            {
                var connection = _connectionFactory.GetDbConnection;
                var sql = "SELECT language_id as 'LanguageId', name FROM languages ORDER BY name";
                var countries = await connection.QueryAsync<LanguageResponse>(sql);

                value = new ApiObject<LanguageResponse>(countries);
                _memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
            }

            return value;
        }

        public async Task<LanguageResponse> GetLanguageAsync(int languageId)
        {
            LanguageResponse value;
            string cacheKey = $"languages:{languageId}";

            if (!_memoryCache.TryGetValue(cacheKey, out value))
            {
                var connection = _connectionFactory.GetDbConnection;
                var sql = "SELECT language_id as 'LanguageId', name FROM languages WHERE language_id = @languageId";
                var countries = await connection.QueryAsync<LanguageResponse>(sql, new { languageId });

                value = countries.SingleOrDefault();
                _memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
            }

            return value;
        }
    }
}
