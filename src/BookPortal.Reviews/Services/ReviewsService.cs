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

        // TODO: Get user rating for this work (from BookPortal.Rating service)
        public async Task<IReadOnlyList<ReviewResponse>> GetReviewsWorkAsync(ReviewRequest reviewRequest)
        {
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

            var queryResult = await query.Select(c => new ReviewResponse
            {
                Id = c.Id,
                Text = c.Text,
                WorkId = c.WorkId,
                DateCreated = c.DateCreated,
                UserId = c.UserId,
            }).ToListAsync();

            // get response votes
            var reviewIds = queryResult.GroupBy(c => c.Id).Select(grp => grp.Key).ToList();
            var reviewVotes = ( from c in _reviewContext.ReviewVotes
                                where reviewIds.Contains(c.ReviewId)
                                group c by c.ReviewId into g
                                select new
                                {
                                    ReviewId = g.Key,
                                    ReviewRating = g.Sum(c => c.Vote)
                                }).ToList();

            // get user names
            var userIds = queryResult.GroupBy(c => c.UserId).Select(grp => grp.Key).ToArray();
            var users = GetUsers(userIds);

            // get work names
            var works = GetWorks(reviewRequest.WorkId);

            foreach (var result in queryResult)
            {
                Work work = works.SingleOrDefault(c => c.WorkId == result.WorkId);
                result.WorkName = work?.Name;
                result.WorkRusname = work?.RusName;
                result.UserName = 
                    users.Where(c => c.UserId == result.UserId).Select(c => c.Name).SingleOrDefault();
                result.ReviewRating =
                    reviewVotes.Where(c => c.ReviewId == result.Id).Select(c => c.ReviewRating).SingleOrDefault();
            }

            return queryResult;
        }

        public async Task<Review> GetReviewAsync(int reviewId)
        {
            return await _reviewContext.Reviews.Where(c => c.Id == reviewId).SingleOrDefaultAsync();
        }

        // TODO: Get from BookPortal.Auth service
        private IReadOnlyList<User> GetUsers(params int[] userIds)
        {
            var list = new List<User>(userIds.Length);

            foreach (var userId in userIds)
            {
                list.Add(new User { UserId = userId, Name = "ravenger" });
            }

            return list;
        }

        // TODO: Get from BookPortal.Web service
        private IReadOnlyList<Work> GetWorks(params int[] workIds)
        {
            var list = new List<Work>(workIds.Length);

            foreach (var workId in workIds)
            {
                list.Add(new Work { WorkId = workId, RusName = "Гиперион", Name = "Hyperion" });
            }

            return list;
        }
    }
}
