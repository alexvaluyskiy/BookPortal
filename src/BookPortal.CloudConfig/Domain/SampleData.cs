using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.CloudConfig.Domain.Models;
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
            yield return new ConfigProfile { Name = "BookPortalLogging" };
            yield return new ConfigProfile { Name = "BookPortalReviews" };
            yield return new ConfigProfile { Name = "BookPortalWeb" };
        }

        private static IEnumerable<Config> GetConfigs()
        {
            yield return new Config
            {
                ProfileId = 1,
                Key = "Data:DefaultConnection:ConnectionString",
                Value = "Data Source=PC-OLVAL0;Database=LogsDatabase;User ID=ravenger;Password=qe1dg7bm"
            };

            yield return new Config
            {
                ProfileId = 2,
                Key = "AppSettings:LoggingService",
                Value = "http://localhost:6001"
            };

            yield return new Config
            {
                ProfileId = 2,
                Key = "AppSettings:ApplicationName",
                Value = "BookPortal.Reviews"
            };

            yield return new Config
            {
                ProfileId = 2,
                Key = "Data:DefaultConnection:ConnectionString",
                Value = "Data Source=PC-OLVAL0;Database=ReviewsDatabase;User ID=ravenger;Password=qe1dg7bm"
            };

            yield return new Config
            {
                ProfileId = 3,
                Key = "AppSettings:LoggingService",
                Value = "http://localhost:6001"
            };

            yield return new Config
            {
                ProfileId = 3,
                Key = "AppSettings:ApplicationName",
                Value = "BookPortal.Web"
            };

            yield return new Config
            {
                ProfileId = 3,
                Key = "Data:DefaultConnection:ConnectionString",
                Value = "Data Source=PC-OLVAL0;Database=ReviewsDatabase;User ID=ravenger;Password=qe1dg7bm"
            };
        }
    }
}
