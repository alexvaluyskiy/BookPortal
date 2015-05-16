using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Responses.Domain;
using BookPortal.Responses.Domain.Models;

namespace BookPortal.Responses.Services
{
    public class ResponsesService
    {
        private readonly ResponseContext _responseContext;

        public ResponsesService(ResponseContext responseContext)
        {
            _responseContext = responseContext;
        }

        public async Task<IReadOnlyList<Response>> GetResponsesAsync()
        {
            return await _responseContext.Responses.ToListAsync();
        }

        public async Task<Response> GetResponseAsync(int responseId)
        {
            return await _responseContext.Responses.Where(c => c.Id == responseId).SingleOrDefaultAsync();
        }
    }
}
