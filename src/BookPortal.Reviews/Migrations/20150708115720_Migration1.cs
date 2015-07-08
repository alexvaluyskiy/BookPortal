using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalReviewsMigrations
{
    public partial class Migration1 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    review_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
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
                name: "review_vote",
                columns: table => new
                {
                    reviews_vote_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
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
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("review_vote");
            migration.DropTable("reviews");
        }
    }
}
