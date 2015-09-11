using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services.Interfaces;

namespace BookPortal.Web.Services
{
    public class TranslationsService : ITranslationsService
    {
        private readonly BookContext _bookContext;

        public TranslationsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<TranslationResponse>> GetTranslationsAsync(TranslationRequest request)
        {
            // get translations lsit
            var translationWorksList = await _bookContext.TranslationWorkPersons
                .Where(c => c.PersonId == request.PersonId)
                .Select(c => c.TranslationWorkId)
                .ToListAsync();

            // get all translators
            var translationPersons =   (from twp in _bookContext.TranslationWorkPersons
                                        join p in _bookContext.Persons on twp.PersonId equals p.Id
                                        where translationWorksList.Contains(twp.TranslationWorkId)
                                        select new
                                        {
                                            Id = twp.TranslationWorkId,
                                            PersonId = p.Id,
                                            Name = p.Name,
                                            NameSort = p.NameSort
                                        }).ToList();


            // get translation names
            var translationsNames =    (from a in _bookContext.EditionTranslations
                                        where translationWorksList.Contains(a.TranslationWorkId)
                                        group a by new {a.TranslationWorkId, a.Name} into g
                                        select new
                                        {
                                            Id = g.Key.TranslationWorkId,
                                            Name = g.Key.Name,
                                            Count = g.Count()
                                        }).ToList();

            // get translator works
            var query =     from tw in _bookContext.TranslationWorks
                            join w in _bookContext.Works on tw.WorkId equals w.Id
                            join wt in _bookContext.WorkTypes on w.WorkTypeId equals wt.Id
                            where translationWorksList.Contains(tw.Id)
                            select new TranslationResponse
                            {
                                TranslationWorkId = tw.Id,
                                WorkId = w.Id,
                                WorkName = w.Name,
                                WorkYear = w.Year,
                                TranslationYear = tw.Year,
                                WorkTypeName = wt.Name,
                                WorkTypeNameSingle = wt.NameSingle,
                                WorkTypeLevel = wt.Level,
                                LanguageId = tw.LanguageId
                            };

            var response = await query.ToListAsync();

            // get all persons
            // TODO: EF7 doesn't generate where clause
            var translationWorks = response.Select(w => w.WorkId).ToList();
            var translationAuthors = (from pw in _bookContext.PersonWorks
                                    join p in _bookContext.Persons on pw.PersonId equals p.Id
                                    where translationWorks.Contains(pw.WorkId)
                                    select new
                                    {
                                        WorkId = pw.WorkId,
                                        PersonId = p.Id,
                                        Name = p.Name,
                                        NameSort = p.NameSort
                                    }).ToList();

            foreach (var item in response)
            {
                // adding all possible names to translation
                item.Names = translationsNames
                    .Where(c => c.Id == item.TranslationWorkId)
                    .OrderByDescending(c => c.Count)
                    .Select(c => c.Name)
                    .ToList();

                // adding all translators, except main
                // TODO: exclude main translator
                item.Translators = translationPersons
                    .Where(c => c.Id == item.TranslationWorkId && c.PersonId != request.PersonId)
                    .Select(c => new PersonResponse
                    {
                        PersonId = c.PersonId,
                        Name = c.Name,
                        NameSort = c.NameSort
                    })
                    .ToList();

                // get all authors
                item.Authors = translationAuthors
                    .Where(c => c.WorkId == item.WorkId)
                    .Select(c => new PersonResponse
                    {
                        PersonId = c.PersonId,
                        Name = c.Name,
                        NameSort = c.NameSort
                    })
                    .ToList();
            }

            switch (request.Sort)
            {
                case TranslatorSort.Author:
                    response = response
                        .OrderBy(c => string.Join(", ", c.Authors.Select(d => d.NameSort)))
                        .ThenBy(c => c.WorkTypeLevel)
                        .ToList();
                    break;
                case TranslatorSort.Type:
                    response = response
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => string.Join(", ", c.Authors.Select(d => d.NameSort)))
                        .ToList();
                    break;
                case TranslatorSort.Year:
                    response = response
                        .OrderBy(c => c.TranslationYear)
                        .ThenBy(c => c.WorkTypeLevel)
                        .ThenBy(c => string.Join(", ", c.Authors.Select(d => d.NameSort)))
                        .ToList();
                    break;
            }

            return response;
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
                    response.Translators.Add(new PersonResponse { PersonId = person.PersonId, Name = person.Name });
                }

                responses.Add(response);
            }

            return responses;
        }

        // TODO: read Correct field
        public async Task<IReadOnlyList<EditionResponse>> GetTranslationEditionsAsync(int translationWorkId)
        {
            var query = from te in _bookContext.EditionTranslations
                        join e in _bookContext.Editions on te.EditionId equals e.Id
                        where te.TranslationWorkId == translationWorkId
                        select new EditionResponse
                        {
                            EditionId = e.Id,
                            Name = e.Name,
                            Year = e.Year,
                            Correct = 1
                        };

            return await query.ToListAsync();
        }

        // TODO: rework when EF7 will supports GroupBy
        private async Task<int> FindTranslationId(int workId, params int[] personIds)
        {
            var query = await (from tw in _bookContext.TranslationWorks
                        join twp in _bookContext.TranslationWorkPersons on tw.Id equals twp.TranslationWorkId
                        where tw.WorkId == workId && personIds.Contains(twp.PersonId)
                        select new { tw.Id, twp.PersonId}).ToListAsync();

            int translationId = query
                .GroupBy(c => c.Id)
                .Where(g => g.Select(d => d.PersonId).Distinct().Count() == personIds.Length)
                .Select(g => g.Key)
                .SingleOrDefault();

            return translationId;
        }
    }
}
