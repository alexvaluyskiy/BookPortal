using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class RatingsReviewsMarks : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "autor_rating",
                columns: table => new
                {
                    author_rating_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    marks_count = table.Column(type: "int", nullable: false),
                    marks_weight = table.Column(type: "int", nullable: false),
                    person_id = table.Column(type: "int", nullable: false),
                    rating = table.Column(type: "float", nullable: false),
                    users_count = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorRating", x => x.author_rating_id);
                });
            migration.CreateTable(
                name: "marks",
                columns: table => new
                {
                    mark_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    mark_value = table.Column(type: "int", nullable: false),
                    user_id = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => x.mark_id);
                });
            migration.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    review_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    date_created = table.Column(type: "datetime2", nullable: false),
                    text = table.Column(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.review_id);
                });
            migration.CreateTable(
                name: "work_expect_rating",
                columns: table => new
                {
                    work_expect_rating_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    expect_type = table.Column(type: "int", nullable: false),
                    plan_date = table.Column(type: "nvarchar(max)", nullable: true),
                    users_count = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExpectRating", x => x.work_expect_rating_id);
                });
            migration.CreateTable(
                name: "work_rating",
                columns: table => new
                {
                    work_rating_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    marks_count = table.Column(type: "int", nullable: false),
                    rating = table.Column(type: "float", nullable: false),
                    rating_type = table.Column(type: "nvarchar(50)", nullable: true),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRating", x => x.work_rating_id);
                });
            migration.CreateTable(
                name: "review_votes",
                columns: table => new
                {
                    reviews_vote_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    date_created = table.Column(type: "datetime2", nullable: false),
                    review_id = table.Column(type: "int", nullable: false),
                    user_id = table.Column(type: "int", nullable: false),
                    vote = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewVote", x => x.reviews_vote_id);
                    table.ForeignKey(
                        name: "FK_ReviewVote_Review_ReviewId",
                        columns: x => x.review_id,
                        referencedTable: "reviews",
                        referencedColumn: "review_id");
                });
            migration.AlterColumn(
                name: "name",
                table: "languages",
                type: "nvarchar(50)",
                nullable: true);
            migration.AddColumn(
                name: "correct",
                table: "editions",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migration.AlterColumn(
                name: "name",
                table: "countries",
                type: "nvarchar(50)",
                nullable: true);
            migration.CreateIndex(
                name: "IX_AuthorRating_PersonId",
                table: "autor_rating",
                column: "person_id",
                unique: true);
            migration.CreateIndex(
                name: "IX_Mark_WorkId",
                table: "marks",
                column: "work_id");
            migration.CreateIndex(
                name: "IX_Mark_WorkId_UserId",
                table: "marks",
                columns: new[] { "work_id", "user_id" });
            migration.CreateIndex(
                name: "IX_Review_UserId",
                table: "reviews",
                column: "user_id");
            migration.CreateIndex(
                name: "IX_Review_WorkId",
                table: "reviews",
                column: "work_id");
            migration.CreateIndex(
                name: "IX_ReviewVote_ReviewId",
                table: "review_votes",
                column: "review_id");
            migration.CreateIndex(
                name: "IX_WorkRating_WorkId_RatingType",
                table: "work_rating",
                columns: new[] { "work_id", "rating_type" },
                unique: true);
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "correct", table: "editions");
            migration.DropTable("autor_rating");
            migration.DropTable("marks");
            migration.DropTable("review_votes");
            migration.DropTable("work_expect_rating");
            migration.DropTable("work_rating");
            migration.DropTable("reviews");
            migration.AlterColumn(
                name: "name",
                table: "languages",
                type: "nvarchar(max)",
                nullable: true);
            migration.AlterColumn(
                name: "name",
                table: "countries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
