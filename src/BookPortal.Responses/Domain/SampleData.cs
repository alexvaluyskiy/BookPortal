using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Responses.Domain.Models;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.Responses.Domain
{
    public static class SampleData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<ResponseContext>())
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
            using (var db = serviceProvider.GetService<ResponseContext>())
            {
                foreach (var response in GetResponses())
                {
                    db.Responses.Add(response);
                }
                await db.SaveChangesAsync();

                foreach (var vote in GetResponseVotes())
                {
                    db.ResponseVotes.Add(vote);
                }
                await db.SaveChangesAsync();
            }
        }

        private static IEnumerable<Response> GetResponses()
        {
            yield return new Response { UserId = 1, WorkId = 1, Text = "очень крутой роман"};
            yield return new Response { UserId = 1, WorkId = 1, Text = "фильм лучше" };
        }

        private static IEnumerable<ResponseVote> GetResponseVotes()
        {
            yield return new ResponseVote { ResponseId = 1, UserId = 2, Vote = 1 };
            yield return new ResponseVote { ResponseId = 2, UserId = 2, Vote = 1 };
            yield return new ResponseVote { ResponseId = 1, UserId = 24, Vote = -1 };
        }
    }
}
