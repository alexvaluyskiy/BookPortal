using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Dapper;
using Microsoft.Framework.Caching.Memory;

namespace BookPortal.Web.Repositories
{
    public class WorkTypesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IMemoryCache _memoryCache;

        public WorkTypesRepository(IConnectionFactory connectionFactory, IMemoryCache memoryCache)
        {
            _connectionFactory = connectionFactory;
            _memoryCache = memoryCache;
        }

        public async Task<ApiObject<WorkTypeResponse>> GetWorkTypesListAsync()
        {
            ApiObject<WorkTypeResponse> value;
            string cacheKey = "worktypes:list";

            if (!_memoryCache.TryGetValue(cacheKey, out value))
            {
                using (var connection = _connectionFactory.Create())
                {
                    var sql = @"
                    SELECT work_type_id as 'WorkTypeId', name, name_single as 'NameSingle', level, is_node as 'IsNode'
                    FROM work_types";

                    var workTypes = await connection.QueryAsync<WorkTypeResponse>(sql);

                    value = new ApiObject<WorkTypeResponse>(workTypes);
                    _memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)});
                }
            }

            return value;
        }

        public async Task<Dictionary<int, WorkTypeResponse>> GetWorkTypesDictionaryAsync()
        {
            var list = await GetWorkTypesListAsync();
            return list.Values.ToDictionary(c => c.WorkTypeId, c => c);
        }

        public async Task<WorkTypeResponse> GetWorkTypeAsync(int workTypeId)
        {
            WorkTypeResponse value;
            string cacheKey = $"worktypes:list:{workTypeId}";

            if (!_memoryCache.TryGetValue(cacheKey, out value))
            {
                using (var connection = _connectionFactory.Create())
                {
                    var sql = @"
                    SELECT work_type_id as 'WorkTypeId', name, name_single as 'NameSingle', level, is_node as 'IsNode'
                    FROM work_types
                    WHERE work_type_id = @workTypeId";

                    var workTypes = await connection.QueryAsync<WorkTypeResponse>(sql, new { workTypeId });

                    value = workTypes.SingleOrDefault();
                    _memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
                }
            }

            return value;
        }
    }
}
