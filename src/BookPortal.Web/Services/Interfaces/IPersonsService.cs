using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface IPersonsService : IBusinessService
    {
        Task<ApiObject<PersonResponse>> GetPeopleAsync(PersonRequest request);
        Task<PersonResponse> GetPersonAsync(int personId);
    }
}