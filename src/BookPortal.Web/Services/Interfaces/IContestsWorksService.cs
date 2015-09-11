using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IContestsWorksService : IBusinessService
    {
        Task<ApiObject<ContestWorkResponse>> GetContestsWorksAsync(int contestId);
        Task<ContestWorkResponse> GetContestWorkAsync(int contestId, int contestWorkId);
    }
}