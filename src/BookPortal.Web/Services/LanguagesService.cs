using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class LanguagesService
    {
        private readonly BookContext _bookContext;

        public LanguagesService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<ApiObject<LanguageResponse>> GetLanguagesAsync()
        {
            var query = _bookContext.Languages
                .Select(c => new LanguageResponse
                {
                    LanguageId = c.Id,
                    Name = c.Name
                });

            var result = new ApiObject<LanguageResponse>();
            result.Values = await query.ToListAsync();
            result.TotalRows = result.Values.Count;

            return result;
        }

        public Task<LanguageResponse> GetLanguageAsync(int id)
        {
            var query = _bookContext.Languages
                .Where(c => c.Id == id)
                .Select(c => new LanguageResponse
                {
                    LanguageId = c.Id,
                    Name = c.Name
                });

            return query.SingleOrDefaultAsync();
        }
    }
}
