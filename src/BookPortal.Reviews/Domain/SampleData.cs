using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Reviews.Domain.Models;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Net.Http.Server;

namespace BookPortal.Reviews.Domain
{
    public static class SampleData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<ReviewContext>())
            {
                var sqlServerDatabase = db.Database as SqlServerDatabase;
                if (sqlServerDatabase != null)
                {
                    await sqlServerDatabase.EnsureDeletedAsync();
                    await sqlServerDatabase.EnsureCreatedAsync();
                }
                await InsertTestData(serviceProvider);
            }
        }

        private static async Task InsertTestData(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<ReviewContext>())
            {
                foreach (var response in GetResponses())
                {
                    db.Reviews.Add(response);
                }
                await db.SaveChangesAsync();

                foreach (var vote in GetResponseVotes())
                {
                    db.ReviewVotes.Add(vote);
                }
                await db.SaveChangesAsync();
            }
        }

        private static IEnumerable<Review> GetResponses()
        {
            yield return new Review { UserId = 1, WorkId = 1, Text = "очень крутой роман"};
            yield return new Review { UserId = 1, WorkId = 1, Text = "фильм лучше" };
        }

        private static IEnumerable<ReviewVote> GetResponseVotes()
        {
            yield return new ReviewVote { ReviewId = 1, UserId = 2, Vote = 1 };
            yield return new ReviewVote { ReviewId = 2, UserId = 2, Vote = 1 };
            yield return new ReviewVote { ReviewId = 1, UserId = 24, Vote = -1 };
        }
    }
}
