using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IRatingsService : IBusinessService
    {
        Task<ApiObject<AuthorRatingResponse>> GetAuthorsRating();
        Task<ApiObject<WorkRatingResponse>> GetWorkRating(string type);
        Task<ApiObject<WorkExpectRatingResponse>> GetWorkExpectRating(string type);
    }
}