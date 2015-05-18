using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Reviews.Domain;
using BookPortal.Reviews.Domain.Models;

namespace BookPortal.Reviews.Services
{
    public class ReviewsService
    {
        private readonly ReviewContext _reviewContext;

        public ReviewsService(ReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public async Task<IReadOnlyList<Review>> GetResponsesAsync()
        {
            return await _reviewContext.Reviews.ToListAsync();
        }

        public async Task<Review> GetResponseAsync(int responseId)
        {
            return await _reviewContext.Reviews.Where(c => c.Id == responseId).SingleOrDefaultAsync();
        }
    }
}
