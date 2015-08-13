using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Domain;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Domain.Models.Types;
using BookPortal.Web.Models.Responses;
using Dapper;

namespace BookPortal.Web.Services
{
    public class WorksService
    {
        private readonly BookContext _bookContext;
        private readonly IConnectionFactory _connectionFactory;

        public WorksService(BookContext bookContext, IConnectionFactory connectionFactory)
        {
            _bookContext = bookContext;
            _connectionFactory = connectionFactory;
        }

        public async Task<ApiObject<WorkResponse>> GetWorksAsync(int personId, string sortMode, bool includeTree)
        {
            using (var connection = _connectionFactory.DbConnection)
            {
                // get all work's ids
                var workIdsSql = @"SELECT work_id as 'WorkId' FROM person_works WHERE person_id = @personId";
                var workIds = (await connection.QueryAsync<int>(workIdsSql, new { personId })).ToList();

                // get all works
                var workSql = @"
					SELECT
                        w.work_id as 'WorkId',
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
                        wt.work_type_id as 'WorkTypeId',
                        wt.name as 'WorkTypeName',
                        wt.[level] as 'WorkTypeLevel'
                    FROM works AS w
                    INNER JOIN work_types AS wt ON w.work_type_id = wt.work_type_id
                    WHERE w.work_id IN @workIds";

                var works = (await connection.QueryAsync<WorkResponse>(workSql, new { workIds })).ToList();

                // get all work's people
                var peopleSql = @"
                    SELECT pw.work_id as 'WorkId', p.person_id as 'PersonId', p.name as 'Name', p.name_sort as 'NameSort', pw.type as 'PersonType'
                    FROM persons AS p
                    INNER JOIN person_works AS pw ON p.person_id = pw.person_id
                    WHERE pw.work_id IN @workIds AND pw.person_id != @personId";

                var people = await connection.QueryAsync(() => new
                                {
                                    WorkId = default(int),
                                    PersonId = default(int),
                                    Name = default(string),
                                    NameSort = default(string),
                                    PersonType = default(WorkPersonType)
                                }, peopleSql, new {workIds, personId});

                var peopleDic = people.GroupBy(c => c.WorkId).ToDictionary(c => c.Key, c => c.Select(d => new PersonResponse
                {
                    PersonId = d.PersonId,
                    Name = d.Name,
                    NameSort = d.NameSort,
                    PersonType = d.PersonType
                }).ToList());

                foreach (var work in works)
                {
                    work.Persons = peopleDic.GetValueOrDefault(work.WorkId);

                    // combining poems into the one type
                    if (work.WorkTypeId == 5 || work.WorkTypeId == 28 || work.WorkTypeId == 29)
                    {
                        work.WorkTypeId = 27;
                    }
                }

                switch (sortMode)
                {
                    case "rusname":
                        works = works
                            .OrderBy(c => c.WorkTypeLevel)
                            .ThenBy(c => c.RusName)
                            .ThenBy(c => c.GroupIndex)
                            .ToList();
                        break;
                    case "name":
                        works = works
                            .OrderBy(c => c.WorkTypeLevel)
                            .ThenBy(c => c.Name)
                            .ThenBy(c => c.GroupIndex)
                            .ToList();
                        break;
                    default:
                        works = works
                            .OrderBy(c => c.WorkTypeLevel)
                            .ThenBy(c => c.Year)
                            .ThenBy(c => c.GroupIndex)
                            .ThenBy(c => c.Name)
                            .ThenBy(c => c.RusName)
                            .ToList();
                        break;
                }

                return new ApiObject<WorkResponse>(works);
            }
        }

        // TODO: add IsPlan, NotPublished, Published fields
        public async Task<ApiObject<WorkResponse>> GetWorksAsync2(int personId, string sortMode)
        {
            var workLinks = await BuildWorksTree(personId);

            var workIds = (from pw in _bookContext.PersonWorks
                           where pw.PersonId == personId
                           select pw.WorkId).ToList();

            var workQuery = from w in _bookContext.Works
                            join wt in _bookContext.WorkTypes on w.WorkTypeId equals wt.Id
                            where workIds.Contains(w.Id)
                            select new WorkResponse
                            {
                                WorkId = w.Id,
                                RusName = w.RusName,
                                Name = w.Name,
                                AltName = w.AltName,
                                Year = w.Year,
                                Description = w.Description,
                                WorkTypeId = wt.Id,
                                WorkTypeName = wt.Name,
                                WorkTypeLevel = wt.Level
                            };

            var works = await workQuery.ToListAsync();

            var persons = (from p in _bookContext.Persons
                           join pw in _bookContext.PersonWorks on p.Id equals pw.PersonId
                           where workIds.Contains(pw.WorkId)
                           select new
                           {
                               WorkId = pw.WorkId,
                               PersonId = p.Id,
                               Name = p.Name,
                               NameSort = p.NameSort,
                               LinkType = pw.Type
                           }).ToList();

            foreach (var work in works)
            {
                // authors plan
                if (work.InPlans.HasValue)
                {
                    work.WorkTypeId = -2;
                    work.WorkTypeLevel = 0;
                }
                else if (work.PublishType.HasValue)
                {
                    work.WorkTypeId = -1;
                    work.WorkTypeLevel = 100;
                }

                // combining poems into the one type
                if (work.WorkTypeId == 5 || work.WorkTypeId == 28 || work.WorkTypeId == 29)
                {
                    work.WorkTypeId = 27;
                }

                var childIds = workLinks.Where(c => c.Key == work.WorkId).Select(c => c.Value.WorkId).ToList();
                work.ChildWorks = new List<int>();
                work.ChildWorks.AddRange(childIds);

                work.Persons = persons
                    .Where(c => c.WorkId == work.WorkId && c.PersonId != personId)
                    .Select(c => new PersonResponse
                    {
                        PersonId = c.PersonId,
                        Name = c.Name,
                        NameSort = c.NameSort
                    }).ToList();
            }

            switch (sortMode)
            {
                case "rusname":
                    works = works
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.RusName)
                        .ThenBy(c => c.GroupIndex)
                        .ToList();
                    break;
                case "name":
                    works = works
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.Name)
                        .ThenBy(c => c.GroupIndex)
                        .ToList();
                    break;
                default:
                    works = works
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.Year)
                        .ThenBy(c => c.GroupIndex)
                        .ThenBy(c => c.Name)
                        .ThenBy(c => c.RusName)
                        .ToList();
                    break;
            }

            return new ApiObject<WorkResponse>(works);
        }

        public async Task<WorkResponse> GetWorkAsync(int workId)
        {
            using (var connection = _connectionFactory.DbConnection)
            {
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
                    var people = await connection.QueryAsync<PersonResponse>(peopleSql, new {workId});
                    work.Persons = people.ToList();
                }

                return work;
            }
        }

        public async Task<MarkResponse> GetWorkMarkAsync(int workId, int userId)
        {
            using (var connection = _connectionFactory.DbConnection)
            {
                var workSql = @"
                    SELECT work_id as 'WorkId', COUNT(*) as 'MarksCount', ROUND(AVG(Cast(mark_value as Float)), 5) as 'Rating'
                    FROM marks
                    WHERE work_id = @workId
                    GROUP BY work_id";
                var query = await connection.QueryAsync<MarkResponse>(workSql, new { workId });
                var workMark = query.SingleOrDefault();

                if (workMark != null && userId > 0)
                {
                    var userMarkSql = @"
                        SELECT mark_value
                        FROM marks
                        WHERE work_id = @workId and user_id = @userId";
                    var userMark = await connection.QueryAsync<int>(userMarkSql, new { workId, userId });
                    workMark.UserMark = userMark.SingleOrDefault();
                }

                return workMark;
            }
        }

        private async Task<Dictionary<int, WorkLink>> BuildWorksTree(int personId)
        {
            using (var connection = _connectionFactory.DbConnection)
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
