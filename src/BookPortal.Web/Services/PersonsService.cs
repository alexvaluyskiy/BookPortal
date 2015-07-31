using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services
{
    public class PersonsService
    {
        private readonly BookContext _bookContext;

        public PersonsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<ApiObject<PersonResponse>> GetPeopleAsync(PersonRequest request)
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

            if (request.IsOpened)
            {
                // TODO: implement person is_opened
            }

            if (request.Offset > 0)
                query = query.Skip(request.Offset);

            if (request.Limit > 0)
                query = query.Take(request.Limit);

            int totalRows = await _bookContext.Persons.CountAsync();
            var result = new ApiObject<PersonResponse>(await query.ToListAsync(), totalRows);

            return result;
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
    }
}
