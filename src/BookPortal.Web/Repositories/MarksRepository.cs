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

        public async Task<MarkResponse> GetWorkMarkAsync(int workId, int userId)
        {
            using (var connection = _connectionFactory.Create())
            {
                var workSql = @"
                    SELECT work_id as 'WorkId', marks_count as 'MarksCount', rating as 'Rating'
                    FROM work_stats
                    WHERE work_id = @workId";

                var query = await connection.QueryAsync<MarkResponse>(workSql, new { workId });
                var workMark = query.SingleOrDefault();

                if (workMark != null && userId > 0)
                {
                    var userMarkSql = @"
                        SELECT mark_value as 'UserMark'
                        FROM marks
                        WHERE work_id = @workId and user_id = @userId";
                    var userMark = await connection.QueryAsync<int>(userMarkSql, new { workId, userId });
                    workMark.UserMark = userMark.SingleOrDefault();
                }

                return workMark;
            }
        }

        public async Task<Dictionary<int, MarkResponse>> GetPersonMarksAsync(List<int> workIds, int? userId)
        {
            using (var connection = _connectionFactory.Create())
            {
                var workSql = @"
                    SELECT work_id as 'WorkId', marks_count as 'MarksCount', rating as 'Rating'
                    FROM work_stats
                    WHERE work_id IN @workIds";

                var query = await connection.QueryAsync<MarkResponse>(workSql, new { workIds });
                var workMarks = query.ToDictionary(c => c.WorkId, c => c);

                if (workMarks.Count > 0 && userId > 0)
                {
                    var userMarkSql = @"
                        SELECT work_id as 'WorkId', mark_value as 'UserMark'
                        FROM marks
                        WHERE work_id IN @workIds and user_id = @userId";

                    var queryMarks = await connection.QueryAsync<MarkResponse>(userMarkSql, new { workIds, userId });
                    foreach (var userMark in queryMarks)
                    {
                        MarkResponse workMarkTemp;
                        workMarks.TryGetValue(userMark.WorkId, out workMarkTemp);
                        if (workMarkTemp != null)
                        {
                            workMarks[userMark.WorkId].UserMark = userMark.UserMark;
                        }
                    }
                }

                return workMarks;
            }
        }
    }
}
