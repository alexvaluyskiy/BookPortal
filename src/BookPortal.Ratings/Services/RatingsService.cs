using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Ratings.Domain;
using BookPortal.Ratings.Domain.Models;
using BookPortal.Ratings.Models;
using Microsoft.Data.Entity;

namespace BookPortal.Ratings.Services
{
    public class RatingsService
    {
        private readonly RatingsContext _ratingsContext;

        public RatingsService(RatingsContext ratingsContext)
        {
            _ratingsContext = ratingsContext;
        }

        public async Task<ApiObject<AuthorRatingResponse>> GetAuthorsRating()
        {
            var ratings = _ratingsContext.AuthorRatings.Select(c => new AuthorRatingResponse
            {
                PersonId = c.PersonId,
                Rating = c.Rating,
                MarksWeight = c.MarksWeight,
                MarksCount = c.MarksCount,
                UsersCount = c.UsersCount
            });

            var apiObject = new ApiObject<AuthorRatingResponse>();
            apiObject.Values = await ratings.ToListAsync();
            apiObject.TotalRows = apiObject.Values.Count;

            return apiObject;
        }

        public Task<WorkRatingResponse> GetWorkRating(int type)
        {
            throw new System.NotImplementedException();
        }
    }
}
