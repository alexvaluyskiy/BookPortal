using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class PersonsService
    {
        private readonly BookContext _bookContext;

        public PersonsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public virtual async Task<IReadOnlyList<Person>> GetPersonsAsync()
        {
            return await _bookContext.Persons.ToListAsync();
        }

        public virtual async Task<Person> GetPersonAsync(int personId)
        {
            return await _bookContext.Persons.Where(c => c.Id == personId).SingleOrDefaultAsync();
        }
    }
}
