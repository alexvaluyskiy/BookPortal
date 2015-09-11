using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IReviewsService : IBusinessService
    {
        Task<ReviewResponse> GetReviewAsync(int reviewId);
        Task<ApiObject<ReviewResponse>> GetReviewsByPersonAsync(ReviewPersonRequest reviewRequest);
        Task<ApiObject<ReviewResponse>> GetReviewsByWorkAsync(ReviewWorkRequest reviewRequest);
    }
}