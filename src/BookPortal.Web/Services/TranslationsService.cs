using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
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

        public async Task<List<TranslationResponse>> GetTranslationsAsync(TranslationRequest request)
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
                                            Person = p
                                        }).ToList();


            // get translation names
            var translationsNames =    (from a in _bookContext.TranslationEditions
                                        where translationWorksList.Contains(a.TranslationWorkId)
                                        group a by new {a.TranslationWorkId, a.Name} into g
                                        select new
                                        {
                                            Id = g.Key.TranslationWorkId,
                                            Name = g.Key.Name,
                                            Count = g.Count()
                                        }).ToList();

            // get all editions
            var translationsEditions = (from te in _bookContext.TranslationEditions
                                        join e in _bookContext.Editions on te.EditionId equals e.Id
                                        where translationWorksList.Contains(te.TranslationWorkId)
                                        select new
                                        {
                                            Id = te.TranslationWorkId,
                                            EditionId = e.Id
                                        }).ToList();

            // get translator works
            var query =     from tw in _bookContext.TranslationWorks
                            join w in _bookContext.Works on tw.WorkId equals w.Id
                            join wt in _bookContext.WorkTypes on w.WorkTypeId equals wt.Id
                            where translationWorksList.Contains(tw.Id)
                            select new TranslationResponse
                            {
                                Id = tw.Id,
                                WorkId = w.Id,
                                WorkName = w.Name,
                                WorkYear = w.Year,
                                TranslationYear = tw.Year,
                                WorkTypeName = wt.Name,
                                WorkTypeNameSingle = wt.NameSingle,
                                WorkTypeLevel = wt.Level
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
                                        Person = p
                                    }).ToList();

            foreach (var item in response)
            {
                // adding all possible names to translation
                item.Names = translationsNames
                    .Where(c => c.Id == item.Id)
                    .OrderByDescending(c => c.Count)
                    .Select(c => c.Name)
                    .ToList();

                // adding all editions
                item.Editions = translationsEditions
                    .Where(c => c.Id == item.Id)
                    .Select(c => c.EditionId)
                    .ToList();

                // adding all translators, except main
                item.Translators = translationPersons
                    .Where(c => c.Id == item.Id)
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
    }
}
