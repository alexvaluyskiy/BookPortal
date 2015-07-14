using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class TranslationsService
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
                                        where translationWorksList.Contains(twp.TranslationWorkId)
                                        join p in _bookContext.Persons on twp.PersonId equals p.Id
                                        select new
                                        {
                                            Id = twp.TranslationWorkId,
                                            Person = new PersonResponse
                                            {
                                                PersonId = p.Id,
                                                Name = p.Name,
                                                NameSort = p.NameSort
                                            }
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
            var translationWorks = response.Select(w => w.WorkId).ToList();
            var translationAuthors = (from pw in _bookContext.PersonWorks
                                    join p in _bookContext.Persons on pw.PersonId equals p.Id
                                    where translationWorks.Contains(pw.WorkId)
                                    select new
                                    {
                                        WorkId = pw.WorkId,
                                        Person = new PersonResponse
                                        {
                                            PersonId = p.Id,
                                            Name = p.Name,
                                            NameSort = p.NameSort
                                        }
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
                    .Where(c => c.Id == item.TranslationWorkId && c.Person.PersonId != request.PersonId)
                    .Select(c => c.Person)
                    .ToList();

                // get all authors
                item.Authors = translationAuthors
                    .Where(c => c.WorkId == item.WorkId)
                    .Select(c => c.Person)
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

        // TODO: read Corrent field
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
    }
}
