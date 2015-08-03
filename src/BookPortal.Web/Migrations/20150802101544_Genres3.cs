using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class Genres3 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AlterColumn(
                name: "parent_genre_work_id",
                table: "genre_works",
                type: "int",
                nullable: true);
            migration.CreateIndex(
                name: "IX_GenrePersonView_PersonId_GenreWorkId",
                table: "genre_persons_view",
                columns: new[] { "person_id", "genre_work_id" },
                unique: true);
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropIndex(name: "IX_GenrePersonView_PersonId_GenreWorkId", table: "genre_persons_view");
            migration.AlterColumn(
                name: "parent_genre_work_id",
                table: "genre_works",
                type: "int",
                nullable: false);
        }
    }
}
