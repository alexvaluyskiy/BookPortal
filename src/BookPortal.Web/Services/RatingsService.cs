using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Microsoft.Framework.Caching.Distributed;
using Microsoft.Framework.Caching.Redis;
using Newtonsoft.Json;
using Microsoft.Data.Entity;
using System.Linq;

namespace BookPortal.Web.Services
{
    public class RatingsService
    {
        private readonly BookContext _bookContext;
        private readonly RedisCache _cache;

        public RatingsService(BookContext bookContext, RedisCache cache)
        {
            _bookContext = bookContext;
            _cache = cache;
        }

        public async Task<ApiObject<AuthorRatingResponse>> GetAuthorsRating()
        {
            var ratings = _bookContext.AuthorRatingsView.Select(c => new AuthorRatingResponse
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
            var cacheEntryBytes = await _cache.GetAsync($"ratings:works:{type}");

            List<WorkRatingResponse> workRating;

            if (cacheEntryBytes == null)
            {
                var ratings = _bookContext.WorkRatingView
                            .Where(c => c.RatingType == type)
                            .Select(c => new WorkRatingResponse
                            {
                                WorkId = c.WorkId,
                                WorkRusName = "not implemented yet", // TODO: not implemented yet
                                WorkName = "not implemented yet", // TODO: not implemented yet
                                WorkYear = -1, // TODO: not implemented yet
                                Persons = new List<PersonResponse> // TODO: not implemented yet
                                {
                                    new PersonResponse
                                    {
                                        PersonId = -1,
                                        Name = "not implemented yet",
                                        NameOriginal = "not implemented yet"
                                    }
                                },
                                Rating = c.Rating,
                                MarksCount = c.MarksCount
                            });

                workRating = await ratings.ToListAsync();
                string serialized = JsonConvert.SerializeObject(workRating);
                await _cache.SetAsync($"ratings:works:{type}", Encoding.UTF8.GetBytes(serialized), 
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)});
            }
            else
            {
                workRating = JsonConvert.DeserializeObject<List<WorkRatingResponse>>(Encoding.UTF8.GetString(cacheEntryBytes));
            }

            var apiObject = new ApiObject<WorkRatingResponse>();
            apiObject.Values = workRating;
            apiObject.TotalRows = apiObject.Values.Count;

            return apiObject;
        }

        public Task<ApiObject<WorkExpectRatingResponse>> GetWorkExpectRating(string type)
        {
            throw new NotImplementedException();
        }
    }
}
