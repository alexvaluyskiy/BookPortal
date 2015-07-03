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
            modelBuilder.Entity<Config>().ToTable("configs");
            modelBuilder.Entity<Config>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("config_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Key).HasColumnName("key").HasColumnType("nvarchar(50)");
                builder.Property(c => c.Value).HasColumnName("value").HasColumnType("nvarchar(250)");
                builder.Property(c => c.ProfileId).HasColumnName("profile_id");
            });

            modelBuilder.Entity<ConfigProfile>().ToTable("profiles");
            modelBuilder.Entity<ConfigProfile>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("profile_id").UseSqlServerIdentityColumn(); ;
                builder.Property(c => c.Name).HasColumnName("name").HasColumnType("nvarchar(50)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
