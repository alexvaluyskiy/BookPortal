using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface INominationsService : IBusinessService
    {
        Task<ApiObject<NominationResponse>> GetNominationsAsync(int awardId);
        Task<NominationResponse> GetNominationAsync(int awardId, int nominationId);
    }
}