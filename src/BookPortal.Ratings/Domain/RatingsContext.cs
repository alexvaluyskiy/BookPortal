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

        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewVote> ReviewVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorRating>().Property(c => c.AuthorRatingId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<AuthorRating>().Index(c => c.PersonId).Unique();

            modelBuilder.Entity<WorkExpectRating>().Property(c => c.WorkExpectRatingId).UseSqlServerIdentityColumn();

            modelBuilder.Entity<WorkRating>().Property(c => c.WorkRatingId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<WorkRating>().Property(c => c.RatingType).HasColumnType("nvarchar(50");
            modelBuilder.Entity<WorkRating>().Index(c => new { c.WorkId, c.RatingType }).Unique();

            modelBuilder.Entity<Mark>().Property(c => c.MarkId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Mark>().Index(c => c.WorkId);
            modelBuilder.Entity<Mark>().Index(c => new { c.WorkId, c.UserId });

            modelBuilder.Entity<Review>().Property(c => c.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Review>().Index(c => c.WorkId);
            modelBuilder.Entity<Review>().Index(c => c.UserId);

            modelBuilder.Entity<ReviewVote>().Property(c => c.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<ReviewVote>().Index(c => c.ReviewId);
        }
    }
}
