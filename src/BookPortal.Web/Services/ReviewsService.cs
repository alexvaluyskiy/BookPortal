using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Models.Types;
using Microsoft.Data.Entity;

namespace BookPortal.Web.Services
{
    public class ReviewsService
    {
        private readonly BookContext _bookContext;

        public ReviewsService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<ReviewResponse> GetReviewAsync(int reviewId)
        {
            var review = await (from r in _bookContext.Reviews
                               join w in _bookContext.Works on r.WorkId equals w.Id
                               where r.Id == reviewId
                               select new ReviewResponse
                               {
                                   ReviewId = r.Id,
                                   Text = r.Text,
                                   WorkId = r.WorkId,
                                   WorkName = w.Name,
                                   WorkRusName = w.RusName,
                                   DateCreated = r.DateCreated,
                                   UserId = r.UserId,
                                   UserName = "not implemented yet" // TODO: not implemented yet
                               }).SingleOrDefaultAsync();

            // get review vote
            review.ReviewRating = await _bookContext.ReviewVotes.Where(c => c.ReviewId == reviewId).SumAsync(c => c.Vote);

            return review;
        }

        // TODO: rework when EF7 will support GroupBy and Navigation Properties
        public async Task<ApiObject<ReviewResponse>> GetReviewsByPersonAsync(ReviewPersonRequest reviewRequest)
        {
            // get all person works
            var workIds = await _bookContext.PersonWorks
                .Where(c => c.PersonId == reviewRequest.PersonId)
                .Select(c => c.WorkId)
                .ToListAsync();

            var reviews = await (from r in _bookContext.Reviews
                                 join w in _bookContext.Works on r.WorkId equals w.Id
                                 where workIds.Contains(r.WorkId)
                                 select new ReviewResponse
                                 {
                                     ReviewId = r.Id,
                                     Text = r.Text,
                                     WorkId = r.WorkId,
                                     WorkName = w.Name,
                                     WorkRusName = w.RusName,
                                     DateCreated = r.DateCreated,
                                     UserId = r.UserId,
                                     UserName = "not implemented yet" // TODO: not implemented yet
                                 }).ToListAsync();

            // get response votes
            var reviewIds = reviews.Select(c => c.ReviewId).ToList();
            var reviewVotes = await _bookContext.ReviewVotes
                                    .Where(c => reviewIds.Contains(c.ReviewId))
                                    .GroupBy(c => c.ReviewId)
                                    .Select(g => new
                                    {
                                        ReviewId = g.Key,
                                        ReviewRating = g.Sum(c => c.Vote)
                                    }).ToDictionaryAsync(c => c.ReviewId, c => c.ReviewRating);

            foreach (var review in reviews)
            {
                int ratingValue;
                reviewVotes.TryGetValue(review.ReviewId, out ratingValue);
                review.ReviewRating = ratingValue;
            }

            switch (reviewRequest.Sort)
            {
                case ReviewSort.Date:
                    reviews = reviews.OrderByDescending(c => c.DateCreated).ToList();
                    break;
                case ReviewSort.Rating:
                    reviews = reviews.OrderBy(c => c.ReviewRating).ThenByDescending(c => c.DateCreated).ToList();
                    break;
            }

            if (reviewRequest.Offset > 0)
                reviews = reviews.Skip(reviewRequest.Offset).ToList();

            if (reviewRequest.Limit > 0)
                reviews = reviews.Take(reviewRequest.Offset).ToList();

            var apiObject = new ApiObject<ReviewResponse>();
            apiObject.Values = reviews;
            apiObject.TotalRows = apiObject.Values.Count;

            return apiObject;
        }

        // TODO: rework when EF7 will support GroupBy and Navigation Properties
        public async Task<ApiObject<ReviewResponse>> GetReviewsByWorkAsync(ReviewWorkRequest reviewRequest)
        {
            var reviews = await (from r in _bookContext.Reviews
                                 join w in _bookContext.Works on r.WorkId equals w.Id
                                 where r.WorkId == reviewRequest.WorkId
                                 select new ReviewResponse
                                 {
                                     ReviewId = r.Id,
                                     Text = r.Text,
                                     WorkId = r.WorkId,
                                     WorkName = w.Name,
                                     WorkRusName = w.RusName,
                                     DateCreated = r.DateCreated,
                                     UserId = r.UserId,
                                     UserName = "not implemented yet" // TODO: not implemented yet
                                 }).ToListAsync();

            // get response votes
            var reviewIds = reviews.Select(c => c.ReviewId).ToList();
            var reviewVotes = await _bookContext.ReviewVotes
                                    .Where(c => reviewIds.Contains(c.ReviewId))
                                    .GroupBy(c => c.ReviewId)
                                    .Select(g => new
                                    {
                                        ReviewId = g.Key,
                                        ReviewRating = g.Sum(c => c.Vote)
                                    }).ToDictionaryAsync(c => c.ReviewId, c => c.ReviewRating);

            foreach (var review in reviews)
            {
                int ratingValue;
                reviewVotes.TryGetValue(review.ReviewId, out ratingValue);
                review.ReviewRating = ratingValue;
            }

            switch (reviewRequest.Sort)
            {
                case ReviewSort.Date:
                    reviews = reviews.OrderByDescending(c => c.DateCreated).ToList();
                    break;
                case ReviewSort.Rating:
                    reviews = reviews.OrderBy(c => c.ReviewRating).ThenByDescending(c => c.DateCreated).ToList();
                    break;
            }

            if (reviewRequest.Offset > 0)
                reviews = reviews.Skip(reviewRequest.Offset).ToList();

            if (reviewRequest.Limit > 0)
                reviews = reviews.Take(reviewRequest.Offset).ToList();

            var apiObject = new ApiObject<ReviewResponse>();
            apiObject.Values = reviews;
            apiObject.TotalRows = apiObject.Values.Count;

            return apiObject;
        }
    }
}
