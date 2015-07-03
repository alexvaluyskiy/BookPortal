using BookPortal.Reviews.Domain.Models;
using Microsoft.Data.Entity;

namespace BookPortal.Reviews.Domain
{
    public class ReviewContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewVote> ReviewVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().ToTable("reviews");
            modelBuilder.Entity<Review>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("review_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.UserId).HasColumnName("user_id");
                builder.Property(c => c.WorkId).HasColumnName("work_id");
                builder.Property(c => c.Text).HasColumnName("text");
                builder.Property(c => c.DateCreated).HasColumnName("date_created");
            });

            modelBuilder.Entity<ReviewVote>().ToTable("review_vote");
            modelBuilder.Entity<ReviewVote>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("reviews_vote_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.ReviewId).HasColumnName("review_id");
                builder.Property(c => c.UserId).HasColumnName("user_id");
                builder.Property(c => c.Vote).HasColumnName("vote");
                builder.Property(c => c.DateCreated).HasColumnName("date_created");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
