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
            modelBuilder.Entity<Review>().ForSqlServer().Table("reviews");
            modelBuilder.Entity<Review>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("review_id");
                builder.Property(c => c.UserId).ForSqlServer().Column("user_id");
                builder.Property(c => c.WorkId).ForSqlServer().Column("work_id");
                builder.Property(c => c.Text).ForSqlServer().Column("text");
                builder.Property(c => c.DateCreated).ForSqlServer().Column("date_created");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<ReviewVote>().ForSqlServer().Table("review_vote");
            modelBuilder.Entity<ReviewVote>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("review_vote_id");
                builder.Property(c => c.ReviewId).ForSqlServer().Column("review_id");
                builder.Property(c => c.UserId).ForSqlServer().Column("user_id");
                builder.Property(c => c.Vote).ForSqlServer().Column("vote");
                builder.Property(c => c.DateCreated).ForSqlServer().Column("date_created");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
