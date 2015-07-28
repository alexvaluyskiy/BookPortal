using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class LanguagesService
    {
        private readonly BookContext _bookContext;

        public LanguagesService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<Language>> GetLanguagesAsync()
        {
            return await _bookContext.Languages.ToListAsync();
        }

        public Task<Language> GetLanguageAsync(int id)
        {
            return _bookContext.Languages.Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}
