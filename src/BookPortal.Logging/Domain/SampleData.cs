using System;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Relational;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.Logging.Domain
{
    public static class SampleData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<LogsContext>())
            {
                var sqlServerDatabase = db.Database as RelationalDatabase;
                if (sqlServerDatabase != null)
                {
                    await sqlServerDatabase.EnsureDeletedAsync();
                    await sqlServerDatabase.EnsureCreatedAsync();
                }
            }
        }
    }
}
