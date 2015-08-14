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
using BookPortal.Web.Repositories;
using Dapper;

namespace BookPortal.Web.Services
{
    public class WorksService
    {
        private readonly WorksRepository _worksRepository;
        private readonly WorkTypesRepository _workTypesRepository;
        private readonly BookContext _bookContext;
        private readonly IConnectionFactory _connectionFactory;

        public WorksService(
            WorksRepository worksRepository,
            WorkTypesRepository workTypesRepository,
            BookContext bookContext,
            IConnectionFactory connectionFactory)
        {
            _worksRepository = worksRepository;
            _workTypesRepository = workTypesRepository;
            _bookContext = bookContext;
            _connectionFactory = connectionFactory;
        }

        public async Task<ApiObject<WorkResponse>> GetWorksAsync(int personId)
        {
            var worktypes = await _workTypesRepository.GetWorkTypesDictionaryAsync();

            var workLinks = await BuildWorksTree(personId);
            var workIds = workLinks.Select(c => c.WorkId).Distinct().ToList();

            using (var connection = _connectionFactory.Create())
            {
                // get all works
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

                var worksRaw = (await connection.QueryAsync<Work>(workSql, new { workIds })).ToList();

                // get all work's people
                var peopleSql = @"
                    SELECT pw.work_id as 'WorkId', p.person_id as 'PersonId', p.name as 'Name', pw.type as 'PersonType'
                    FROM persons AS p
                    INNER JOIN person_works AS pw ON p.person_id = pw.person_id
                    WHERE pw.work_id IN @workIds AND pw.person_id != @personId";

                var people = await connection.QueryAsync(() => new
                                {
                                    WorkId = default(int),
                                    PersonId = default(int),
                                    Name = default(string),
                                    PersonType = default(WorkPersonType)
                                }, peopleSql, new {workIds, personId});

                var peopleDic = people.GroupBy(c => c.WorkId).ToDictionary(c => c.Key, c => c.Select(d => new PersonResponse
                {
                    PersonId = d.PersonId,
                    Name = d.Name,
                    PersonType = d.PersonType
                }).ToList());

                List<WorkResponse> works = new List<WorkResponse>();
                foreach (var work in worksRaw)
                {
                    var workType = worktypes[work.WorkTypeId];
                    // manual restriction to show the work in biblio
                    if (work.ShowInBiblio == 2) continue;
                    // restrict to show magazines
                    if (work.WorkTypeId == 26) continue;

                    var workLink = workLinks.SingleOrDefault(c => c.WorkId == work.Id && c.LinkType == 2);

                    var workResponse = CreateWorkResponse(work, workType, peopleDic, workLink);

                    var childWorks = workLinks.Where(c => c.ParentWorkId == work.Id).ToList();
                    if ((work.ShowSubworksInBiblio == 1 || (work.ShowSubworksInBiblio != 2 && workType.IsNode) || work.WorkTypeId == 50)
                        && childWorks.Count > 0)
                    {
                        workResponse.ChildWorks = new List<WorkResponse>();
                        foreach (var child in childWorks)
                        {
                            var childWork = worksRaw.SingleOrDefault(c => c.Id == child.WorkId);
                            var childWorkType = worktypes[childWork.WorkTypeId];

                            var childWorkResponse = CreateWorkResponse(childWork, childWorkType, peopleDic, null);

                            workResponse.ChildWorks.Add(childWorkResponse);
                        }
                    }

                    works.Add(workResponse);
                }

                return new ApiObject<WorkResponse>(works);
            }
        }

        private WorkResponse CreateWorkResponse(Work work, WorkTypeResponse workType, Dictionary<int, List<PersonResponse>> peopleDic, WorkLink workLink)
        {
            var workResponse = new WorkResponse();
            workResponse.WorkId = work.Id;
            workResponse.RusName = work.RusName;
            workResponse.Name = work.Name;
            workResponse.AltName = work.AltName;
            workResponse.Year = work.Year;
            workResponse.WorkTypeLevel = workType.Level;
            workResponse.WorkTypeName = workType.NameSingle;
            workResponse.InPlans = work.InPlans;
            workResponse.Persons = peopleDic.GetValueOrDefault(work.Id);

            // TODO: coauthors type
            if (workResponse.Persons != null && workResponse.Persons.Count > 0)
            {
                workResponse.CoAuthorType = "coauthor";
            }

            // TODO: check for feminine or masculine form
            if (work.NotFinished)
            {
                workResponse.NotFinished = "не окончен";
            }

            switch (work.PublishType)
            {
                case PublishType.NotPublished:
                    workResponse.PublishType = "не опубликован";
                    break;
                case PublishType.Network:
                    workResponse.PublishType = "сетевая публикация";
                    break;
                case PublishType.Other:
                    workResponse.PublishType = "доступен в сети";
                    break;
            }

            if (workLink != null)
            {
                workResponse.GroupIndex = workLink.GroupIndex;
                workResponse.IsAddition = workLink.IsAddition;
                workResponse.BonusText = workLink.BonusText;
            }

            workResponse.VotesCount = 1000;
            workResponse.Rating = 5.78;
            workResponse.UserMark = 6;


            workResponse.RootCycleWorkId = null;
            workResponse.RootCycleWorkName = null;
            workResponse.RootCycleWorkTypeId = null;

            return workResponse;
        }

        public async Task<WorkResponse> GetWorkAsync(int workId)
        {
            using (var connection = _connectionFactory.Create())
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
            using (var connection = _connectionFactory.Create())
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

        private async Task<List<WorkLink>> BuildWorksTree(int personId)
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
                // var workLinks = query.Where(c => c.ParentWorkId.HasValue).ToDictionary(c => c.ParentWorkId.Value, c => c);

                return query.ToList();
            }
        }
    }
}
