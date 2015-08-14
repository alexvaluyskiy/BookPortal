using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Dapper;

namespace BookPortal.Web.Repositories
{
    public class WorkTypesRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public WorkTypesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<ApiObject<WorkTypeResponse>> GetWorkTypesListAsync()
        {
            using (var connection = _connectionFactory.Create())
            {
                var sql = @"
                    SELECT work_type_id as 'WorkTypeId', name, name_single as 'NameSingle', level, is_node as 'IsNode'
                    FROM work_types";

                var workTypes = await connection.QueryAsync<WorkTypeResponse>(sql);

                return new ApiObject<WorkTypeResponse>(workTypes);
            }
        }

        public async Task<Dictionary<int, WorkTypeResponse>> GetWorkTypesDictionaryAsync()
        {
            var list = await GetWorkTypesListAsync();
            return list.Values.ToDictionary(c => c.WorkTypeId, c => c);
        }

        public async Task<WorkTypeResponse> GetWorkTypeAsync(int workTypeId)
        {
            using (var connection = _connectionFactory.Create())
            {
                var sql = @"
                    SELECT work_type_id as 'WorkTypeId', name, name_single as 'NameSingle', level, is_node as 'IsNode'
                    FROM work_types
                    WHERE work_type_id = @workTypeId";

                var workTypes = await connection.QueryAsync<WorkTypeResponse>(sql, new { workTypeId });

                return workTypes.SingleOrDefault();
            }
        }
    }
}
