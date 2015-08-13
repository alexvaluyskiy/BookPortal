using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Dapper;

namespace BookPortal.Web.Services
{
    public class WorkTypesService
    {
        private readonly IConnectionFactory _connectionFactory;

        public WorkTypesService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<ApiObject<WorkTypeResponse>> GetWorkTypesListAsync()
        {
            using (var connection = _connectionFactory.Create())
            {
                var sql = @"
                    SELECT work_type_id as 'WorkTypeId', name, name_single as 'NameSingle', level
                    FROM work_types";

                var workTypes = await connection.QueryAsync<WorkTypeResponse>(sql);

                return new ApiObject<WorkTypeResponse>(workTypes);
            }
        }

        public async Task<WorkTypeResponse> GetWorkTypeAsync(int workTypeId)
        {
            using (var connection = _connectionFactory.Create())
            {
                var sql = @"
                    SELECT work_type_id as 'WorkTypeId', name, name_single as 'NameSingle', level
                    FROM work_types
                    WHERE work_type_id = @workTypeId";

                var workTypes = await connection.QueryAsync<WorkTypeResponse>(sql, new { workTypeId });

                return workTypes.SingleOrDefault();
            }
        }
    }
}
