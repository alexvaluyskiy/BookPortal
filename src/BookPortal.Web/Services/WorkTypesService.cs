using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Repositories;
using BookPortal.Web.Services.Interfaces;

namespace BookPortal.Web.Services
{
    public class WorkTypesService : IWorkTypesService
    {
        private readonly WorkTypesRepository _workTypesRepository;

        public WorkTypesService(WorkTypesRepository workTypesRepository)
        {
            _workTypesRepository = workTypesRepository;
        }

        public Task<ApiObject<WorkTypeResponse>> GetWorkTypesListAsync()
        {
            return _workTypesRepository.GetWorkTypesListAsync();
        }

        public Task<WorkTypeResponse> GetWorkTypeAsync(int workTypeId)
        {
            return _workTypesRepository.GetWorkTypeAsync(workTypeId);
        }
    }
}
