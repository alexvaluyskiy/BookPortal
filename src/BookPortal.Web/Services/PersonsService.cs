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

        public virtual async Task<IReadOnlyList<Person>> GetPeopleAsync(PersonRequest request)
        {
            var query = _bookContext.Persons.AsQueryable();

            if (request.Offset > 0)
                query = query.Skip(request.Offset);

            if (request.Limit > 0)
                query = query.Take(request.Limit);

            return await query.ToListAsync();
        }

        public virtual async Task<int> GetPeopleCountsAsync(PersonRequest request)
        {
            var query = _bookContext.Persons.AsQueryable();

            return await query.CountAsync();
        }

        public virtual async Task<Person> GetPersonAsync(int personId)
        {
            return await _bookContext.Persons.Where(c => c.Id == personId).SingleOrDefaultAsync();
        }
    }
}
