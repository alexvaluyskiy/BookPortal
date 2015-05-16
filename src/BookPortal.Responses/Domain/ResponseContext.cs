using BookPortal.Responses.Domain.Models;
using Microsoft.Data.Entity;

namespace BookPortal.Responses.Domain
{
    public class ResponseContext : DbContext
    {
        public DbSet<Response> Responses { get; set; }
        public DbSet<ResponseVote> ResponseVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Response>().ForRelational().Table("responses");
            modelBuilder.Entity<Response>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("response_id");
                builder.Property(c => c.UserId).ForRelational().Column("user_id");
                builder.Property(c => c.WorkId).ForRelational().Column("work_id");
                builder.Property(c => c.Text).ForRelational().Column("text");
                builder.Property(c => c.DateCreated).ForRelational().Column("date_created");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<ResponseVote>().ForRelational().Table("response_votes");
            modelBuilder.Entity<ResponseVote>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("response_vote_id");
                builder.Property(c => c.ResponseId).ForRelational().Column("response_id");
                builder.Property(c => c.UserId).ForRelational().Column("user_id");
                builder.Property(c => c.Vote).ForRelational().Column("vote");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
