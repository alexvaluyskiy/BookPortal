using System;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.Logging.Domain
{
    public static class SampleData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<LogsContext>())
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();
            }
        }
    }
}
