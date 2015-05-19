using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Reviews.Domain;
using BookPortal.Reviews.Domain.Models;
using BookPortal.Reviews.Model;

namespace BookPortal.Reviews.Services
{
    public class ReviewsService
    {
        private readonly ReviewContext _reviewContext;

        public ReviewsService(ReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public async Task<IReadOnlyList<ReviewResponse>> GetReviewsWorkAsync(ReviewRequest reviewRequest)
        {
            // TODO: Get from BookPortal.Auth service
            var users = new Dictionary<int, string>() { { 1, "ravenger" } };

            // TODO: Get from BookPortal.Web service
            var workName = new { WorkName = "Hyperion", WorkRusName = "Гиперион" };

            var query = _reviewContext.Reviews.Where(c => c.WorkId == reviewRequest.WorkId);

            switch (reviewRequest.Sort)
            {
                case ReviewSort.Date:
                    query = query.OrderByDescending(c => c.DateCreated);
                    break;
                case ReviewSort.Rating:
                    query = query.OrderByDescending(c => c.DateCreated);
                    break;
            }

            if (reviewRequest.Offset.HasValue && reviewRequest.Offset.Value > 0)
                query = query.Skip(reviewRequest.Offset.Value);

            if (reviewRequest.Limit.HasValue && reviewRequest.Limit > 0)
                query = query.Take(reviewRequest.Limit.Value);

            var queryResult = query.Select(c => new ReviewResponse
            {
                Id = c.Id,
                Text = c.Text,
                WorkId = c.WorkId,
                WorkRusname = workName.WorkRusName,
                WorkName = workName.WorkName,
                DateCreated = c.DateCreated,
                UserId = c.UserId,
                UserName = users[c.UserId]
            });

            return await queryResult.ToListAsync();
        }

        public async Task<Review> GetReviewAsync(int reviewId)
        {
            return await _reviewContext.Reviews.Where(c => c.Id == reviewId).SingleOrDefaultAsync();
        }
    }
}
