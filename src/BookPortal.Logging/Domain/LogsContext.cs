using Microsoft.Data.Entity;

namespace BookPortal.Logging.Domain
{
    public class LogsContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().ForSqlServer().Table("logs");
            modelBuilder.Entity<Log>(builder =>
            {
                builder.Key(c => c.OperationContext);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
