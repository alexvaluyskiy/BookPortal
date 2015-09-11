using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IPublishersService : IBusinessService
    {
        Task<PublisherResponse> GetPublisherAsync(int publisherId);
    }
}