using System.Threading.Tasks;
using BookPortal.Web.Domain;
using Dapper;

namespace BookPortal.Web.Services.Internal
{
    public class InitialJobs
    {
        private readonly IConnectionFactory _connectionFactory;

        public InitialJobs(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitWorkStats()
        {
            using (var connection = _connectionFactory.Create())
            {
                var sql = @"
                    INSERT INTO work_stats (work_id, marks_count, rating)
                    SELECT work_id, COUNT(*) as 'marks_count', ROUND(AVG(Cast(mark_value as Float)), 5) as 'rating'
                    FROM marks
                    GROUP BY work_id
                    ORDER BY work_id";

                await connection.ExecuteAsync(sql);
            }
        }
    }
}
