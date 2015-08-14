using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using Dapper;

namespace BookPortal.Web.Repositories
{
    public class WorksRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public WorksRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Dictionary<int, WorkLink>> BuildWorksTreeAsync(int personId)
        {
            using (var connection = _connectionFactory.Create())
            {
                var sql = @"
                    WITH tree AS
                    (
                        SELECT wl.work_link_id, wl.work_id, wl.parent_work_id, wl.link_type, wl.is_addition, wl.group_index, wl.bonus_text
	                    FROM work_links wl JOIN person_works pw ON pw.work_id = wl.work_id
	                    WHERE pw.person_id = @personId
                        UNION ALL
                        SELECT wl.work_link_id, wl.work_id, wl.parent_work_id, wl.link_type, wl.is_addition, wl.group_index, wl.bonus_text
	                    FROM work_links wl JOIN tree t ON wl.work_id = t.parent_work_id
                    )
                    SELECT
					    DISTINCT work_id as 'WorkId', parent_work_id as 'ParentWorkId', link_type as 'LinkType', is_addition as 'IsAddition',
					    group_index as 'GroupIndex', bonus_text as 'BonusText'
				    FROM tree";

                var query = await connection.QueryAsync<WorkLink>(sql, new { personId });
                var workLinks = query.Where(c => c.ParentWorkId.HasValue).ToDictionary(c => c.ParentWorkId.Value, c => c);

                return workLinks;
            }
        }
    }
}
