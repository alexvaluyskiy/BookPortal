using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Ratings.Domain;
using BookPortal.Ratings.Models;
using BookPortal.Ratings.Models.Shims;
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
                PersonName = "not implemented yet", // TODO: not implemented yet
                PersonNameOriginal = "not implemented yet", // TODO: not implemented yet
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

        public async Task<ApiObject<WorkRatingResponse>> GetWorkRating(string type)
        {
            var ratings = _ratingsContext.WorkRating
                .Where(c => c.RatingType == type)
                .Select(c => new WorkRatingResponse
                {
                    WorkId = c.WorkId,
                    WorkRusName = "not implemented yet", // TODO: not implemented yet
                    WorkName = "not implemented yet", // TODO: not implemented yet
                    WorkYear = -1, // TODO: not implemented yet
                    Persons = new List<PersonResponseShim> // TODO: not implemented yet
                    {
                        new PersonResponseShim
                        {
                            PersonId = -1,
                            Name = "not implemented yet", 
                            NameOriginal = "not implemented yet"
                        }
                    },
                    Rating = c.Rating,
                    MarksCount = c.MarksCount
                });

            var apiObject = new ApiObject<WorkRatingResponse>();
            apiObject.Values = await ratings.ToListAsync();
            apiObject.TotalRows = apiObject.Values.Count;

            return apiObject;
        }

        public Task<ApiObject<WorkExpectRatingResponse>> GetWorkExpectRating(string type)
        {
            throw new System.NotImplementedException();
        }
    }
}
