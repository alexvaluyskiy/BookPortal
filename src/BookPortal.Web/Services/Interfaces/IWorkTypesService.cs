using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IWorkTypesService : IBusinessService
    {
        Task<ApiObject<WorkTypeResponse>> GetWorkTypesListAsync();
        Task<WorkTypeResponse> GetWorkTypeAsync(int workTypeId);
    }
}