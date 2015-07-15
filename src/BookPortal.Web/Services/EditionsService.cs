using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Models;

namespace BookPortal.Web.Services
{
    public class EditionsService
    {
        private readonly BookContext _bookContext;

        public EditionsService(BookContext bookContext)
        {
            _bookContext = bookContext;
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
    }
}
