using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Dapper;

namespace BookPortal.Web.Repositories
{
    public class MarksRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public MarksRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<MarkResponse>> GetWorkMarkAsync(params int[] workIds)
        {
            var connection = _connectionFactory.GetDbConnection;
            var workSql = @"
                    SELECT work_id as 'WorkId', marks_count as 'MarksCount', rating as 'Rating'
                    FROM work_stats
                    WHERE work_id IN @workIds";

            var query = await connection.QueryAsync<MarkResponse>(workSql, new { workIds });

            return query.ToList();
        }

        public async Task<List<MarkResponse>> GetUserMarkAsync(int userId, params int[] workIds)
        {
            var connection = _connectionFactory.GetDbConnection;
            var workSql = @"
                    SELECT work_id as 'WorkId', mark_value as 'UserMark'
                    FROM marks
                    WHERE work_id IN @workIds and user_id = @userId";

            var query = await connection.QueryAsync<MarkResponse>(workSql, new { workIds, userId });

            return query.ToList();
        }
    }
}
