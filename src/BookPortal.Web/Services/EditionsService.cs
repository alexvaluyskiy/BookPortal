using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Repositories;

namespace BookPortal.Web.Services
{
    public class EditionsService
    {
        private readonly BookContext _bookContext;
        private readonly EditionsRepository _editionsRepository;

        public EditionsService(BookContext bookContext, EditionsRepository editionsRepository)
        {
            _bookContext = bookContext;
            _editionsRepository = editionsRepository;
        }

        public async Task<EditionResponse> GetEditionAsync(int editionId)
        {
            var query = _bookContext.Editions
                .Where(c => c.Id == editionId)
                .Select(c => new EditionResponse
                {
                    EditionId = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Authors = c.Authors,
                    Compilers = c.Compilers,
                    Isbn = c.Isbn,
                    Year = c.Year,
                    ReleaseDate = c.ReleaseDate,
                    Count = c.Count,
                    CoverType = c.CoverType,
                    SuperCover = c.SuperCover,
                    Format = c.Format,
                    Pages = c.Pages,
                    Description = c.Description,
                    Content = c.Content,
                    Notes = c.Notes,
                    LanguageId = c.LanguageId
                });

            return await query.SingleOrDefaultAsync();
        }

        public async Task<ApiObject<EditionResponse>> GetEditionsByPersonAsync(int personId)
        {
            var editions = from e in _bookContext.Editions
                           join ew in _bookContext.EditionWorks on e.Id equals ew.EditionId
                           join w in _bookContext.PersonWorks on ew.WorkId equals w.WorkId
                           where w.PersonId == personId
                           select new EditionResponse
                           {
                               EditionId = e.Id,
                               Name = e.Name,
                               Year = e.Year,
                               Correct = 1
                           };

            return new ApiObject<EditionResponse>(await editions.ToListAsync());
        }

        // TODO: for works page create simplier versions of this API (last five translated)
        // TODO: created extended versions with grouping and sorting
        public async Task<ApiObject<EditionResponse>> GetEditionsByWorkAsync(int workId)
        {
            var editions = from e in _bookContext.Editions
                           join ew in _bookContext.EditionWorks on e.Id equals ew.EditionId
                           where ew.WorkId == workId
                           select new EditionResponse
                           {
                               EditionId = e.Id,
                               Name = e.Name,
                               Year = e.Year,
                               Correct = 1
                           };

            return new ApiObject<EditionResponse>(await editions.ToListAsync());
        }

        // TODO: add paging
        public async Task<ApiObject<EditionResponse>> GetEditionsByPublisherAsync(int publisherId)
        {
            var editions = from e in _bookContext.Editions
                           join ep in _bookContext.EditionPublishers on e.Id equals ep.EditionId
                           where ep.PublisherId == publisherId
                           select new EditionResponse
                           {
                               EditionId = e.Id,
                               Name = e.Name,
                               Year = e.Year,
                               Correct = 1
                           };

            return new ApiObject<EditionResponse>(await editions.ToListAsync());
        }

        // TODO: read NameSort from database
        // TODO: read fields: Correct, Publishers
        // TODO: read edition type from service
        public async Task<ApiObject<EditionResponse>> GetEditionsBySerieAsync(SerieRequest request)
        {
            var editions = await _editionsRepository.GetEditionsBySerieAsync(request.SerieId);

            var regexAuthors = new Regex(@"(?:\[autor\=(?<id>\d+)]|)(?<name>[A-zА-я\s]+)(?:\[\/autor\]|)(?:,|$)", RegexOptions.Compiled);
            var regexDescription = new Regex(@"\[(?:\/|)[A-zА-я0-9=:]+\]", RegexOptions.Compiled);
            foreach (var edition in editions)
            {
                var matches = regexAuthors.Matches(edition.Authors);
                edition.Authors = null;
                edition.Persons = new List<PersonResponse>();
                foreach (Match match in matches)
                {
                    PersonResponse person = new PersonResponse();
                    person.PersonId = match.Groups["id"].Success ? int.Parse(match.Groups["id"].Value) : 0;
                    person.Name = match.Groups["name"].Success ? match.Groups["name"].Value.Trim() : string.Empty;

                    if (person.Name == "Антология")
                    {
                        person.NameSort = string.Empty;
                    }

                    if (true)
                    {
                        person.NameSort = string.Join(" ", person.Name.Split(' ').Reverse());
                    }
                    edition.Persons.Add(person);
                }

                edition.Description = regexDescription.Replace(edition.Description, "");
            }

            // var personIds = editions.SelectMany(c => c.Persons.Select(p => p.Id)).Distinct();

            IOrderedEnumerable<EditionResponse> sortedEditions;
            switch (request.Sort)
            {
                case EditionsSort.Name:
                    sortedEditions = editions.OrderBy(c => c.Name).ThenBy(c => c.SerieSort).ThenBy(c => c.Year);
                    break;
                case EditionsSort.Authors:
                    sortedEditions = editions.OrderBy(c => string.Join(", ", c.Persons.Select(p => p.NameSort))).ThenBy(c => c.SerieSort).ThenBy(c => c.Year);
                    break;
                case EditionsSort.Year:
                    sortedEditions = editions.OrderBy(c => c.Year).ThenBy(c => c.SerieSort).ThenBy(c => c.Name);
                    break;
                default:
                    sortedEditions = editions.OrderByDescending(c => c.Year).ThenBy(c => c.SerieSort);
                    break;
            }

            List<EditionResponse> resultEditions;
            if (request.Limit > 0 && request.Offset > 0)
            {
                resultEditions = sortedEditions.Skip(request.Offset).Take(request.Limit).ToList();
            }
            else
            {
                resultEditions = sortedEditions.Take(request.Limit).ToList();
            }
                
            return new ApiObject<EditionResponse>(resultEditions, editions.Count);
        }
    }
}
