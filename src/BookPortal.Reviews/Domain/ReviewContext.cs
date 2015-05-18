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
            modelBuilder.Entity<Review>().ForRelational().Table("reviews");
            modelBuilder.Entity<Review>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("response_id");
                builder.Property(c => c.UserId).ForRelational().Column("user_id");
                builder.Property(c => c.WorkId).ForRelational().Column("work_id");
                builder.Property(c => c.Text).ForRelational().Column("text");
                builder.Property(c => c.DateCreated).ForRelational().Column("date_created");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<ReviewVote>().ForRelational().Table("review_vote");
            modelBuilder.Entity<ReviewVote>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("response_vote_id");
                builder.Property(c => c.ReviewId).ForRelational().Column("response_id");
                builder.Property(c => c.UserId).ForRelational().Column("user_id");
                builder.Property(c => c.Vote).ForRelational().Column("vote");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
