using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public virtual async Task<IReadOnlyList<Language>> GetLanguagesAsync()
        {
            return await _bookContext.Languages.ToListAsync();
        }

        public virtual Task<Language> GetLanguageAsync(int id)
        {
            return _bookContext.Languages.Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}
