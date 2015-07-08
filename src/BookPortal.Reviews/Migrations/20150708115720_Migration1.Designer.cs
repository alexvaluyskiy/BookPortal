using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using BookPortal.Reviews.Domain;

namespace BookPortalReviewsMigrations
{
    [ContextType(typeof(ReviewContext))]
    partial class Migration1
    {
        public override string Id
        {
            get { return "20150708115720_Migration1"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13698"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13698")
                .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

            builder.Entity("BookPortal.Reviews.Domain.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "review_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<DateTime>("DateCreated")
                        .Annotation("Relational:ColumnName", "date_created");

                    b.Property<string>("Text")
                        .Annotation("Relational:ColumnName", "text");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "reviews");
                });

            builder.Entity("BookPortal.Reviews.Domain.Models.ReviewVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "reviews_vote_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<DateTime>("DateCreated")
                        .Annotation("Relational:ColumnName", "date_created");

                    b.Property<int>("ReviewId")
                        .Annotation("Relational:ColumnName", "review_id");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Property<int>("Vote")
                        .Annotation("Relational:ColumnName", "vote");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "review_vote");
                });

            builder.Entity("BookPortal.Reviews.Domain.Models.ReviewVote", b =>
                {
                    b.Reference("BookPortal.Reviews.Domain.Models.Review")
                        .InverseCollection()
                        .ForeignKey("ReviewId");
                });
        }
    }
}
