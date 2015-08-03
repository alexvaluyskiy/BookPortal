using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class Genres5 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "order",
                table: "person_works",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migration.CreateIndex(
                name: "IX_PersonWork_WorkId",
                table: "person_works",
                column: "work_id");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropIndex(name: "IX_PersonWork_WorkId", table: "person_works");
            migration.DropColumn(name: "order", table: "person_works");
        }
    }
}
