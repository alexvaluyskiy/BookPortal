using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services.Interfaces
{
    public interface ITranslationsService : IBusinessService
    {
        Task<IReadOnlyList<TranslationResponse>> GetTranslationsAsync(TranslationRequest request);
        Task<IReadOnlyList<TranslationResponse>> GetWorkTranslationsAsync(int workId);
        Task<IReadOnlyList<EditionResponse>> GetTranslationEditionsAsync(int translationWorkId);
    }
}