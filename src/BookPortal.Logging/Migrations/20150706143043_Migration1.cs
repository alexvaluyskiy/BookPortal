using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalLoggingMigrations
{
    public partial class Migration1 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "logs",
                columns: table => new
                {
                    OperationContext = table.Column(type: "uniqueidentifier", nullable: false),
                    Exception = table.Column(type: "nvarchar(max)", nullable: true),
                    Layer = table.Column(type: "nvarchar(max)", nullable: true),
                    Message = table.Column(type: "nvarchar(max)", nullable: true),
                    Severity = table.Column(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.OperationContext);
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("logs");
        }
    }
}
