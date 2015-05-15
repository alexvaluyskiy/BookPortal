using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Models;
using Remotion.Linq.Clauses;

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

            // get translator works
            var query =     from tw in _bookContext.TranslationWorks
                            join twp in _bookContext.TranslationWorkPersons on tw.Id equals twp.TranslationWorkId
                            join p in _bookContext.Persons on twp.PersonId equals p.Id
                            join w in _bookContext.Works on tw.WorkId equals w.Id
                            join wt in _bookContext.WorkTypes on w.WorkTypeId equals wt.Id
                            where translationWorksList.Contains(tw.Id)
                            select new TranslationResponse
                            {
                                Id = tw.Id,
                                PersonId = p.Id,
                                PersonName = p.NameSort,
                                WorkId = w.Id,
                                WorkName = w.Name,
                                WorkYear = w.Year,
                                TranslationYear = tw.Year,
                                WorkTypeName = wt.Name,
                                WorkTypeSingle = wt.NameSingle,
                                WorkTypeLevel = wt.Level
                            };

            switch (request.Sort)
            {
                case TranslatorSort.Author:
                    query = query
                        .OrderBy(c => c.PersonName)
                        .ThenBy(c => c.WorkTypeLevel);
                    break;
                case TranslatorSort.Type:
                    query = query
                        .OrderBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.PersonName);
                    break;
                case TranslatorSort.Year:
                    query = query
                        .OrderBy(c => c.TranslationYear)
                        .ThenBy(c => c.WorkTypeLevel)
                        .ThenBy(c => c.PersonName);
                    break;
            }

            var response = await query.ToListAsync();

            foreach (var item in response)
            {
                // adding all possible names to translation
                item.Names = translationsNames
                    .Where(c => c.Id == 1)
                    .OrderByDescending(c => c.Count)
                    .Select(c => c.Name)
                    .ToList();
            }

            return response;
        }
    }
}
