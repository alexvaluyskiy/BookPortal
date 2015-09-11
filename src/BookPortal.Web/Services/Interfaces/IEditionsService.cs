using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IEditionsService : IBusinessService
    {
        Task<EditionResponse> GetEditionAsync(int editionId);
        Task<ApiObject<EditionResponse>> GetEditionsByPersonAsync(int personId);
        Task<ApiObject<EditionResponse>> GetEditionsByWorkAsync(int workId);
        Task<ApiObject<EditionResponse>> GetEditionsByPublisherAsync(int publisherId);
        Task<ApiObject<EditionResponse>> GetEditionsBySerieAsync(SerieRequest request);
    }
}