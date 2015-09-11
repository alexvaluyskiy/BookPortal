using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IWorksService : IBusinessService
    {
        Task<ApiObject<WorkResponse>> GetWorksAsync(int personId, int userId);
        Task<WorkResponse> GetWorkAsync(int workId);
        Task<MarkResponse> GetWorkMarkAsync(int workId, int userId);
    }
}