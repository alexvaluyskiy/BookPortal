using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class Genres2 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.RenameTable(
                name: "rating_work",
                newName: "rating_work_view");
            migration.RenameTable(
                name: "rating_work_expect",
                newName: "rating_work_expect_view");
            migration.RenameTable(
                name: "rating_author",
                newName: "rating_author_view");
            migration.RenameTable(
                name: "genre_works",
                newName: "genre_works_view");
            migration.RenameTable(
                name: "genre_work",
                newName: "genre_works");
            migration.RenameTable(
                name: "person_genres",
                newName: "genre_persons_view");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.RenameTable(
                name: "rating_work_view",
                newName: "rating_work");
            migration.RenameTable(
                name: "rating_work_expect_view",
                newName: "rating_work_expect");
            migration.RenameTable(
                name: "rating_author_view",
                newName: "rating_author");
            migration.RenameTable(
                name: "genre_works_view",
                newName: "genre_works");
            migration.RenameTable(
                name: "genre_works",
                newName: "genre_work");
            migration.RenameTable(
                name: "genre_persons_view",
                newName: "person_genres");
        }
    }
}
