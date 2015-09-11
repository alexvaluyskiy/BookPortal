using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface ISeriesService : IBusinessService
    {
        Task<IReadOnlyList<Serie>> GetSeriesAsync(int publisherId);
        Task<SerieResponse> GetSerieAsync(int serieId);
        Task<IReadOnlyList<SerieResponse>> GetSerieByPublisher(int publisherId);
        Task<SerieTreeItem> GetSerieTreeAsync(int serieId);
    }
}