using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.CloudConfig.Domain.Models;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.CloudConfig.Domain
{
    public static class SampleData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<ConfigContext>())
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                await InsertTestData(serviceProvider);
            }
        }

        private static async Task InsertTestData(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<ConfigContext>())
            {
                foreach (var profile in GetProfiles())
                {
                    db.Profiles.Add(profile);
                }
                await db.SaveChangesAsync();

                foreach (var config in GetConfigs())
                {
                    db.Configs.Add(config);
                }
                await db.SaveChangesAsync();
            }
        }

        private static IEnumerable<ConfigProfile> GetProfiles()
        {
            yield return new ConfigProfile { Name = "Shared" };
            yield return new ConfigProfile { Name = "BookPortalLogging" };
            yield return new ConfigProfile { Name = "BookPortalReviews" };
            yield return new ConfigProfile { Name = "BookPortalWeb" };
        }

        private static IEnumerable<Config> GetConfigs()
        {
            yield return new Config
            {
                ProfileId = 1,
                Key = "Services:LoggingService",
                Value = "http://localhost:6001"
            };

            yield return new Config
            {
                ProfileId = 2,
                Key = "Data:DefaultConnection:ConnectionString",
                Value = "Data Source=RAVENGER-PC\\SQLEXPRESS;Database=LogsDatabase;User ID=ravenger;Password=qe1dg7bm"
            };

            yield return new Config
            {
                ProfileId = 3,
                Key = "AppSettings:ApplicationName",
                Value = "BookPortal.Reviews"
            };

            yield return new Config
            {
                ProfileId = 3,
                Key = "Data:DefaultConnection:ConnectionString",
                Value = "Data Source=RAVENGER-PC\\SQLEXPRESS;Database=ReviewsDatabase;User ID=ravenger;Password=qe1dg7bm"
            };

            yield return new Config
            {
                ProfileId = 4,
                Key = "AppSettings:ApplicationName",
                Value = "BookPortal.Web"
            };

            yield return new Config
            {
                ProfileId = 4,
                Key = "Data:DefaultConnection:ConnectionString",
                Value = "Data Source=RAVENGER-PC\\SQLEXPRESS;Database=BooksDatabase;User ID=ravenger;Password=qe1dg7bm"
            };

            yield return new Config
            {
                ProfileId = 4,
                Key = "AppSettings:ImportOzonUrl",
                Value = "http://www.ozon.ru/context/detail/id/"
            };
        }
    }
}
