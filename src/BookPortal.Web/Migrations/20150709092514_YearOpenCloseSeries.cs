using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class YearOpenCloseSeries : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropColumn(name: "date_close", table: "series");
            migration.DropColumn(name: "date_open", table: "series");
            migration.DropColumn(name: "date_close", table: "publishers");
            migration.DropColumn(name: "date_open", table: "publishers");
            migration.AddColumn(
                name: "year_close",
                table: "series",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "year_open",
                table: "series",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "year_close",
                table: "publishers",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "year_open",
                table: "publishers",
                type: "int",
                nullable: true);
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "year_close", table: "series");
            migration.DropColumn(name: "year_open", table: "series");
            migration.DropColumn(name: "year_close", table: "publishers");
            migration.DropColumn(name: "year_open", table: "publishers");
            migration.AddColumn(
                name: "date_close",
                table: "series",
                type: "datetime2",
                nullable: true);
            migration.AddColumn(
                name: "date_open",
                table: "series",
                type: "datetime2",
                nullable: true);
            migration.AddColumn(
                name: "date_close",
                table: "publishers",
                type: "datetime2",
                nullable: true);
            migration.AddColumn(
                name: "date_open",
                table: "publishers",
                type: "datetime2",
                nullable: true);
        }
    }
}
