using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IContestsService : IBusinessService
    {
        Task<ApiObject<ContestResponse>> GetContestsAsync(int awardId);
        Task<ContestResponse> GetContestAsync(int awardId, int contestId);
    }
}