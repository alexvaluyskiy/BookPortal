using BookPortal.Ratings.Domain.Models;
using Microsoft.Data.Entity;

namespace BookPortal.Ratings.Domain
{
    public class RatingsContext : DbContext
    {
        public DbSet<AuthorRating> AuthorRatings { get; set; }
        public DbSet<WorkExpectRating> WorkExpectRating { get; set; }
        public DbSet<WorkRating> WorkRating { get; set; }
        public DbSet<Mark> Marks { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorRating>().Property(c => c.AuthorRatingId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<AuthorRating>().Index(c => c.PersonId).Unique();

            modelBuilder.Entity<WorkExpectRating>().Property(c => c.WorkExpectRatingId).UseSqlServerIdentityColumn();

            modelBuilder.Entity<WorkRating>().Property(c => c.WorkRatingId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<WorkRating>().Index(c => c.WorkId).Unique();

            modelBuilder.Entity<Mark>().Property(c => c.MarkId).UseSqlServerIdentityColumn();
        }
    }
}
