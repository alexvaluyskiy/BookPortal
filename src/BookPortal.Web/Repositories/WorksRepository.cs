using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models.Responses;
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

        public async Task<List<WorkLink>> BuildWorksTreeAsync(int personId)
        {
            var connection = _connectionFactory.GetDbConnection;
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

            return query.ToList();
        }

        public async Task<List<Work>> GetWorksByIdsAsync(IEnumerable<int> workIds)
        {
            var connection = _connectionFactory.GetDbConnection;
            var workSql = @"
					SELECT
                        w.work_id as 'Id',
                        w.rusname,
                        w.name,
                        w.altname,
                        w.[year],
                        w.[description],
						w.show_in_biblio as 'ShowInBiblio',
						w.show_subworks_in_biblio as 'ShowSubworksInBiblio',
						w.publish_type as 'PublishType',
						w.not_finished as 'NotFinished',
						w.in_plans as 'InPlans',
                        w.work_type_id as 'WorkTypeId'
                    FROM works AS w
                    WHERE w.work_id IN @workIds";

            var query = await connection.QueryAsync<Work>(workSql, new { workIds });

            return query.ToList();
        }

        public async Task<WorkResponse> GetWorkAsync(int workId)
        {
            var connection = _connectionFactory.GetDbConnection;
            var workSql = @"
                    SELECT
                        w.work_id as 'WorkId', w.rusname, w.name, w.altname, w.year, w.description, w.notes,
                        wt.work_type_id as 'WorkTypeId', wt.name_single as 'WorkTypeName',
                        w.not_finished as 'NotFinished', w.publish_type as 'PublishType', w.in_plans as 'InPlans'
                    FROM works AS w
                    INNER JOIN work_types AS wt ON w.work_type_id = wt.work_type_id
                    WHERE w.work_id = @workId";
            var query = await connection.QueryAsync<WorkResponse>(workSql, new { workId });
            var work = query.SingleOrDefault();

            if (work != null)
            {
                var peopleSql = @"
                        SELECT p.person_id as 'PersonId', p.name, pw.[type] as 'PersonType'
                        FROM persons AS p
                        INNER JOIN person_works AS pw ON p.person_id = pw.person_id
                        WHERE pw.work_id = @workId
                        ORDER BY pw.[order]";
                var people = await connection.QueryAsync<PersonResponse>(peopleSql, new { workId });
                work.Persons = people.ToList();
            }

            return work;
        }
    }
}
