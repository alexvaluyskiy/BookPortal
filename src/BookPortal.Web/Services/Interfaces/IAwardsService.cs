using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IAwardsService : IBusinessService
    {
        Task<AwardResponse> GetAwardAsync(int awardId);
        Task<ApiObject<AwardResponse>> GetAwardsAsync(AwardRequest request);
        Task<IReadOnlyList<AwardItemResponse>> GetPersonAwardsAsync(int personId);
        Task<IReadOnlyList<AwardItemResponse>> GetPublisherAwardsAsync(int publisherId);
        Task<IReadOnlyList<AwardItemResponse>> GetWorkAwardsAsync(int workId);
    }
}