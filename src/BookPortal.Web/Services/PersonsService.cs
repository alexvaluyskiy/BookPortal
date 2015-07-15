using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class PersonsService
    {
        private readonly BookContext _bookContext;

        public PersonsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<PersonResponse>> GetPeopleAsync(PersonRequest request)
        {
            var query = _bookContext.Persons
                .Select(c => new PersonResponse
                {
                    PersonId = c.Id,
                    Name = c.Name,
                    NameRp = c.NameRp,
                    NameOriginal = c.NameOriginal,
                    NameSort = c.NameSort,
                    Gender = (int)c.Gender,
                    Birthdate = c.Birthdate,
                    Deathdate = c.Deathdate,
                    CountryId = c.CountryId,
                    LanguageId = c.LanguageId,
                    Biography = c.Biography,
                    BiographySource = c.BiographySource,
                    Notes = c.Notes
                });

            if (request.Offset > 0)
                query = query.Skip(request.Offset);

            if (request.Limit > 0)
                query = query.Take(request.Limit);

            return await query.ToListAsync();
        }

        public async Task<int> GetPeopleCountsAsync(PersonRequest request)
        {
            var query = _bookContext.Persons.AsQueryable();

            return await query.CountAsync();
        }

        public async Task<PersonResponse> GetPersonAsync(int personId)
        {
            var query = _bookContext.Persons
                .Where(c => c.Id == personId)
                .Select(c => new PersonResponse
                {
                    PersonId = c.Id,
                    Name = c.Name,
                    NameRp = c.NameRp,
                    NameOriginal = c.NameOriginal,
                    NameSort = c.NameSort,
                    Gender = (int)c.Gender,
                    Birthdate = c.Birthdate,
                    Deathdate = c.Deathdate,
                    CountryId = c.CountryId,
                    LanguageId = c.LanguageId,
                    Biography = c.Biography,
                    BiographySource = c.BiographySource,
                    Notes = c.Notes
                });

            return await query.SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<EditionResponse>> GetPersonEditionsAsync(int personId)
        {
            var query = from e in _bookContext.Editions
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

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<ContestWorkResponse>> GetPersonAwardsAsync(int personId)
        {
            // all person awards
            var personAwards = await (from cw in _bookContext.ContestWorks
                                join c in _bookContext.Contests on cw.ContestId equals c.Id
                                join n in _bookContext.Nominations on cw.NominationId equals n.Id
                                join a in _bookContext.Awards on c.AwardId equals a.Id
                                where cw.LinkType == ContestWorkType.Person && cw.LinkId == personId
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
                                }).ToListAsync();

            // all person's works awards
            var workAwards = await (from cw in _bookContext.ContestWorks
                                      join c in _bookContext.Contests on cw.ContestId equals c.Id
                                      join n in _bookContext.Nominations on cw.NominationId equals n.Id
                                      join a in _bookContext.Awards on c.AwardId equals a.Id
                                      join pw in _bookContext.PersonWorks on cw.LinkId equals pw.WorkId
                                      where cw.LinkType == ContestWorkType.Work && pw.PersonId == personId
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
                                      }).ToListAsync();

            personAwards.AddRange(workAwards);

            return personAwards;
        }
    }
}
