using BookPortal.CloudConfig.Domain.Models;
using Microsoft.Data.Entity;

namespace BookPortal.CloudConfig.Domain
{
    public class ConfigContext : DbContext
    {
        public DbSet<Config> Configs { get; set; }

        public DbSet<ConfigProfile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>().ForSqlServer().Table("configs");
            modelBuilder.Entity<Config>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer()
                    .Column("config_id");
                builder.Property(c => c.Key).ForSqlServer()
                    .Column("key")
                    .ColumnType("nvarchar(50)");
                builder.Property(c => c.Value).ForSqlServer()
                    .Column("value")
                    .ColumnType("nvarchar(250)");
                builder.Property(c => c.ProfileId).ForSqlServer()
                    .Column("profile_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<ConfigProfile>().ForSqlServer().Table("profiles");
            modelBuilder.Entity<ConfigProfile>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer()
                    .Column("profile_id");
                builder.Property(c => c.Name).ForSqlServer()
                    .Column("name")
                    .ColumnType("nvarchar(50)");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
