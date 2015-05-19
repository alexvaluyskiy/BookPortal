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
            modelBuilder.Entity<Config>().ForRelational().Table("configs");
            modelBuilder.Entity<Config>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("config_id");
                builder.Property(c => c.Key).ForRelational().Column("key");
                builder.Property(c => c.Value).ForRelational().Column("value");
                builder.Property(c => c.ProfileId).ForRelational().Column("profile_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<ConfigProfile>().ForRelational().Table("profiles");
            modelBuilder.Entity<ConfigProfile>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("profile_id");
                builder.Property(c => c.Name).ForRelational().Column("name");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
