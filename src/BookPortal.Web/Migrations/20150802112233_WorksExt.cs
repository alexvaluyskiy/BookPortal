using System.Collections.Generic;
using BookPortal.Web.Domain.Models.Types;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class WorksExt : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AlterColumn(
                name: "rusname",
                table: "works",
                type: "nvarchar(255)",
                nullable: true);
            migration.AlterColumn(
                name: "name",
                table: "works",
                type: "nvarchar(255)",
                nullable: true);
            migration.AlterColumn(
                name: "altname",
                table: "works",
                type: "nvarchar(255)",
                nullable: true);
            migration.AddColumn(
                name: "in_plans",
                table: "works",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migration.AddColumn(
                name: "publish_type",
                table: "works",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migration.AddColumn(
                name: "show_subworks_in_biblio",
                table: "works",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "in_plans", table: "works");
            migration.DropColumn(name: "publish_type", table: "works");
            migration.DropColumn(name: "show_subworks_in_biblio", table: "works");
            migration.AlterColumn(
                name: "rusname",
                table: "works",
                type: "nvarchar(max)",
                nullable: true);
            migration.AlterColumn(
                name: "name",
                table: "works",
                type: "nvarchar(max)",
                nullable: true);
            migration.AlterColumn(
                name: "altname",
                table: "works",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
