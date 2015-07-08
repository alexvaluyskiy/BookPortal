using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class AddPersonOrder : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.RenameColumn(
                name: "PersonOrder",
                table: "translation_work_persons",
                newName: "person_order");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.RenameColumn(
                name: "person_order",
                table: "translation_work_persons",
                newName: "PersonOrder");
        }
    }
}
