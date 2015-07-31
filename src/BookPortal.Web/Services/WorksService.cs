using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services
{
    public class WorksService
    {
        private readonly BookContext _bookContext;

        public WorksService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<WorkResponse>> GetWorksAsync(int personId, string sortMode)
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
                if (work.IsPlan)
                {
                    work.WorkTypeId = -2;
                    work.WorkTypeLevel = 0;
                }
                else if (work.NotFinished || work.Published)
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

            return works;
        }

        public async Task<WorkResponse> GetWorkAsync(int workId)
        {
            var query = from w in _bookContext.Works
                        join wt in _bookContext.WorkTypes on w.WorkTypeId equals wt.Id
                        where w.Id == workId
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

            return await query.SingleOrDefaultAsync();
        }

        private async Task<Dictionary<int, WorkLink>> BuildWorksTree(int personId)
        {
            var workLinks = new Dictionary<int, WorkLink>();

            var sql = @"
                WITH tree AS
                (
                    SELECT wl.work_link_id, wl.work_id, wl.parent_work_id, wl.link_type, wl.is_addition, wl.group_index, wl.bonus_text
	                FROM work_links wl JOIN person_works pw ON pw.work_id = wl.work_id
	                WHERE pw.person_id = @person_id

                    UNION ALL

                    SELECT wl.work_link_id, wl.work_id, wl.parent_work_id, wl.link_type, wl.is_addition, wl.group_index, wl.bonus_text
	                FROM work_links wl JOIN tree t ON wl.work_id = t.parent_work_id
                )
                SELECT DISTINCT * FROM tree";

            var connection = _bookContext.Database.GetDbConnection() as SqlConnection;
            if (connection != null)
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@person_id", personId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int workId = reader.GetValue<int>("work_id");
                            int? parentWorkId = reader.GetValue<int?>("parent_work_id");

                            if (parentWorkId.HasValue && !workLinks.ContainsKey(parentWorkId.Value))
                            {
                                var workLink = new WorkLink();
                                workLink.WorkId = workId;
                                workLink.ParentWorkId = parentWorkId;
                                workLink.LinkType = reader.GetValue<int>("link_type");
                                workLink.IsAddition = reader.GetValue<bool>("work_id");
                                workLink.BonusText = (string)reader["bonus_text"];
                                workLink.GroupIndex = reader.GetValue<int?>("group_index");

                                workLinks.Add(parentWorkId.Value, workLink);
                            }
                        }
                    }
                }
                connection.Close();
            }

            return workLinks;
        }
    }
}
