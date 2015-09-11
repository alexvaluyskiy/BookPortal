using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IGenresService : IBusinessService
    {
        Task<ApiObject<GenreWorksGroupResponse>> GetGenreWorksGroups();
        Task<ApiObject<GenrePersonResponse>> GetAuthorGenres(int personId);
        Task<ApiObject<GenreWorkResponse>> GetWorkGenres(int workId);
    }
}