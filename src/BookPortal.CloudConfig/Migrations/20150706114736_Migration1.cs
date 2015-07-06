using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalCloudConfigMigrations
{
    public partial class Migration1 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    profile_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    name = table.Column(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigProfile", x => x.profile_id);
                });
            migration.CreateTable(
                name: "configs",
                columns: table => new
                {
                    config_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    key = table.Column(type: "nvarchar(50)", nullable: true),
                    profile_id = table.Column(type: "int", nullable: false),
                    value = table.Column(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.config_id);
                    table.ForeignKey(
                        name: "FK_Config_ConfigProfile_ProfileId",
                        columns: x => x.profile_id,
                        referencedTable: "profiles",
                        referencedColumn: "profile_id");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("configs");
            migration.DropTable("profiles");
        }
    }
}
