using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;

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
            var workIds = (from pw in _bookContext.PersonWorks
                        where pw.PersonId == personId
                        select pw.WorkId).ToList();

            var works = from w in _bookContext.Works
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
            }

            switch (sortMode)
            {
                case "rusname":
                    works = works
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.RusName)
                        .ThenBy(c => c.GroupIndex);
                    break;
                case "name":
                    works = works
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.Name)
                        .ThenBy(c => c.GroupIndex);
                    break;
                default:
                    works = works
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.Year)
                        .ThenBy(c => c.GroupIndex)
                        .ThenBy(c => c.Name)
                        .ThenBy(c => c.RusName);
                    break;
            }

            return await works.ToListAsync();
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

        public async Task<IReadOnlyList<ContestWorkResponse>> GetWorkAwardsAsync(int workId)
        {
            var query = from cw in _bookContext.ContestWorks
                        join c in _bookContext.Contests on cw.ContestId equals c.Id
                        join n in _bookContext.Nominations on cw.NominationId equals n.Id
                        join a in _bookContext.Awards on c.AwardId equals a.Id
                        where cw.LinkType == ContestWorkType.Work && cw.LinkId == workId
                        select new ContestWorkResponse
                        {
                            AwardId = a.Id,
                            AwardRusname = a.RusName,
                            AwardName = a.Name,
                            AwardIsOpened = a.IsOpened,
                            ContestId = c.Id,
                            ContestName = c.Name,
                            ContestYear = c.NameYear,
                            NominationId = n.Id,
                            NominationRusname = n.RusName,
                            NominationName = n.Name,
                            ContestWorkId = cw.Id,
                            ContestWorkRusname = cw.RusName,
                            ContestWorkName = cw.Name,
                            ContestWorkPrefix = cw.Prefix,
                            ContestWorkPostfix = cw.Postfix
                        };

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<EditionResponse>> GetWorkEditionsAsync(int workId)
        {
            var query = from e in _bookContext.Editions
                        join ew in _bookContext.EditionWorks on e.Id equals ew.EditionId
                        where ew.WorkId == workId
                        select new EditionResponse
                        {
                            EditionId = e.Id,
                            Name = e.Name,
                            Year = e.Year,
                            Correct = 1
                        };

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TranslationResponse>> GetWorkTranslationsAsync(int workId)
        {
            // TODO: rework when EF7 will supports GroupBy with InnerJoin
            var sql = @"
                SELECT
                    tw.translation_work_id,
                    et.name
                FROM
                    translation_works tw
                    INNER JOIN edition_translations et ON et.translation_work_id = tw.translation_work_id
                WHERE
                    tw.work_id = @work_id
                GROUP BY
                    tw.translation_work_id,
                    tw.work_id,
                    et.name
                ORDER BY
                    tw.translation_work_id,
                    COUNT(*) DESC";

            List<TranslationResponse> translationNames = new List<TranslationResponse>();
            var connection = _bookContext.Database.GetDbConnection() as SqlConnection;
            if (connection != null)
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@work_id", workId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var name = new TranslationResponse
                            {
                                TranslationWorkId = reader.GetValue<int>("translation_work_id"),
                                WorkName = reader.GetValue<string>("name")
                            };
                            translationNames.Add(name);
                        }
                    }
                }
                connection.Close();
            }

            var query = from tw in _bookContext.TranslationWorks
                        join twp in _bookContext.TranslationWorkPersons on tw.Id equals twp.TranslationWorkId
                        join p in _bookContext.Persons on twp.PersonId equals p.Id
                        where tw.WorkId == workId
                        orderby tw.LanguageId, tw.Year, tw.Id, twp.PersonOrder
                        select new
                        {
                            TranslationWorkId = tw.Id,
                            PersonId = twp.PersonId,
                            Name = p.Name,
                            LanguageId = tw.LanguageId,
                            Year = tw.Year
                        };


            var result = await query.ToListAsync();

            // TODO: try to optimize it later
            List<TranslationResponse> responses = new List<TranslationResponse>();
            foreach (var item in result.GroupBy(c => c.TranslationWorkId).Select(c => c.Key))
            {
                TranslationResponse response = new TranslationResponse();

                var items = result.Where(c => c.TranslationWorkId == item).ToList();

                response.LanguageId = items.First().LanguageId;
                response.TranslationWorkId = item;
                response.TranslationYear = items.First().Year;
                response.Names = translationNames.Where(c => c.TranslationWorkId == item).Select(c => c.WorkName).ToList();

                response.Translators = new List<PersonResponse>();
                foreach (var person in items)
                {
                    response.Translators.Add(new PersonResponse { PersonId = person.PersonId, Name = person.Name});
                }

                responses.Add(response);
            }

            return responses;
        }
    }
}
