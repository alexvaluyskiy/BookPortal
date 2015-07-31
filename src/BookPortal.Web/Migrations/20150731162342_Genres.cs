using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class Genres : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropTable("autor_rating");
            migration.DropTable("work_expect_rating");
            migration.DropTable("work_rating");
            migration.CreateTable(
                name: "person_genres",
                columns: table => new
                {
                    genre_person_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    genre_count = table.Column(type: "int", nullable: false),
                    genre_total = table.Column(type: "int", nullable: false),
                    person_id = table.Column(type: "int", nullable: false),
                    work_genre_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenrePersonView", x => x.genre_person_id);
                    table.ForeignKey(
                        name: "FK_GenrePersonView_Person_PersonId",
                        columns: x => x.person_id,
                        referencedTable: "persons",
                        referencedColumn: "person_id");
                });
            migration.CreateTable(
                name: "genre_work",
                columns: table => new
                {
                    genre_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    genre_work_group_id = table.Column(type: "int", nullable: false),
                    level = table.Column(type: "int", nullable: false),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    parent_genre_work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreWork", x => x.genre_work_id);
                });
            migration.CreateTable(
                name: "genre_works",
                columns: table => new
                {
                    genre_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    genre_count = table.Column(type: "int", nullable: false),
                    work_genre_id = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreWorkView", x => x.genre_work_id);
                    table.ForeignKey(
                        name: "FK_GenreWorkView_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "rating_author",
                columns: table => new
                {
                    rating_author_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    marks_count = table.Column(type: "int", nullable: false),
                    marks_weight = table.Column(type: "int", nullable: false),
                    person_id = table.Column(type: "int", nullable: false),
                    rating = table.Column(type: "float", nullable: false),
                    users_count = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingAuthorView", x => x.rating_author_id);
                    table.ForeignKey(
                        name: "FK_RatingAuthorView_Person_PersonId",
                        columns: x => x.person_id,
                        referencedTable: "persons",
                        referencedColumn: "person_id");
                });
            migration.CreateTable(
                name: "rating_work_expect",
                columns: table => new
                {
                    rating_work_expect_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    expect_type = table.Column(type: "int", nullable: false),
                    plan_date = table.Column(type: "nvarchar(max)", nullable: true),
                    users_count = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingWorkExpectView", x => x.rating_work_expect_id);
                    table.ForeignKey(
                        name: "FK_RatingWorkExpectView_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "rating_work",
                columns: table => new
                {
                    rating_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    marks_count = table.Column(type: "int", nullable: false),
                    rating = table.Column(type: "float", nullable: false),
                    rating_type = table.Column(type: "nvarchar(50)", nullable: true),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingWorkView", x => x.rating_work_id);
                    table.ForeignKey(
                        name: "FK_RatingWorkView_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "genre_work_users",
                columns: table => new
                {
                    genre_work_user_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    date_created = table.Column(type: "datetime2", nullable: false),
                    genre_work_id = table.Column(type: "int", nullable: false),
                    user_id = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreWorkUser", x => x.genre_work_user_id);
                    table.ForeignKey(
                        name: "FK_GenreWorkUser_GenreWork_GenreWorkId",
                        columns: x => x.genre_work_id,
                        referencedTable: "genre_work",
                        referencedColumn: "genre_work_id");
                    table.ForeignKey(
                        name: "FK_GenreWorkUser_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.AddColumn(
                name: "is_opened",
                table: "persons",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migration.CreateIndex(
                name: "IX_RatingAuthorView_PersonId",
                table: "rating_author",
                column: "person_id",
                unique: true);
            migration.CreateIndex(
                name: "IX_RatingWorkView_WorkId_RatingType",
                table: "rating_work",
                columns: new[] { "work_id", "rating_type" },
                unique: true);
            migration.AddForeignKey(
                name: "FK_Review_Work_WorkId",
                table: "reviews",
                column: "work_id",
                referencedTable: "works",
                referencedColumn: "work_id");
            migration.RenameColumn(
                name: "language_id",
                table: "persons",
                newName: "default_language_id");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Review_Work_WorkId", table: "reviews");
            migration.DropColumn(name: "is_opened", table: "persons");
            migration.DropTable("person_genres");
            migration.DropTable("genre_work_users");
            migration.DropTable("genre_works");
            migration.DropTable("rating_author");
            migration.DropTable("rating_work_expect");
            migration.DropTable("rating_work");
            migration.DropTable("genre_work");
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
            migration.CreateIndex(
                name: "IX_AuthorRating_PersonId",
                table: "autor_rating",
                column: "person_id",
                unique: true);
            migration.CreateIndex(
                name: "IX_WorkRating_WorkId_RatingType",
                table: "work_rating",
                columns: new[] { "work_id", "rating_type" },
                unique: true);
            migration.AddForeignKey(
                name: "FK_Serie_Serie_ParentSerieId",
                table: "series",
                column: "parent_serie_id",
                referencedTable: "series",
                referencedColumn: "serie_id");
            migration.RenameColumn(
                name: "default_language_id",
                table: "persons",
                newName: "language_id");
        }
    }
}
