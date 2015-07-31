using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class Init2 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AlterColumn(
                name: "name_single",
                table: "work_types",
                type: "nvarchar(50)",
                nullable: true);
            migration.AlterColumn(
                name: "name",
                table: "work_types",
                type: "nvarchar(50)",
                nullable: true);
            migration.AlterColumn(
                name: "parent_work_id",
                table: "work_links",
                type: "int",
                nullable: true);
            migration.AlterColumn(
                name: "group_index",
                table: "work_links",
                type: "int",
                nullable: true);
            migration.CreateIndex(
                name: "IX_WorkLink_ParentWorkId",
                table: "work_links",
                column: "parent_work_id");
            migration.AlterColumn(
                name: "year",
                table: "works",
                type: "int",
                nullable: true);
            migration.AlterColumn(
                name: "year",
                table: "translation_works",
                type: "int",
                nullable: true);
            migration.CreateIndex(
                name: "IX_TranslationWork_WorkId",
                table: "translation_works",
                column: "work_id");
            migration.CreateIndex(
                name: "IX_PublisherSerie_PublisherId",
                table: "publisher_series",
                column: "publisher_id");
            migration.CreateIndex(
                name: "IX_PersonWork_PersonId",
                table: "person_works",
                column: "person_id");
            migration.AlterColumn(
                name: "name",
                table: "languages",
                type: "nvarchar(max)",
                nullable: true);
            migration.CreateIndex(
                name: "IX_EditionWork_WorkId",
                table: "edition_works",
                column: "work_id");
            migration.CreateIndex(
                name: "IX_EditionSerie_SerieId",
                table: "edition_series",
                column: "serie_id");
            migration.AlterColumn(
                name: "name",
                table: "countries",
                type: "nvarchar(max)",
                nullable: true);
            migration.CreateIndex(
                name: "IX_ContestWork_LinkId_LinkType",
                table: "contest_works",
                columns: new[] { "link_id", "link_type" });
            migration.RenameColumn(
                name: "id",
                table: "edition_works",
                newName: "edition_work_id");
            migration.RenameColumn(
                name: "translation_edition_id",
                table: "edition_translations",
                newName: "edition_translation_id");
            migration.RenameColumn(
                name: "id",
                table: "edition_series",
                newName: "edition_serie_id");
            migration.RenameColumn(
                name: "id",
                table: "edition_publishers",
                newName: "edition_publisher_id");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropIndex(name: "IX_WorkLink_ParentWorkId", table: "work_links");
            migration.DropIndex(name: "IX_TranslationWork_WorkId", table: "translation_works");
            migration.DropIndex(name: "IX_PublisherSerie_PublisherId", table: "publisher_series");
            migration.DropIndex(name: "IX_PersonWork_PersonId", table: "person_works");
            migration.DropIndex(name: "IX_EditionWork_WorkId", table: "edition_works");
            migration.DropIndex(name: "IX_EditionSerie_SerieId", table: "edition_series");
            migration.DropIndex(name: "IX_ContestWork_LinkId_LinkType", table: "contest_works");
            migration.AlterColumn(
                name: "name_single",
                table: "work_types",
                type: "nvarchar(max)",
                nullable: true);
            migration.AlterColumn(
                name: "name",
                table: "work_types",
                type: "nvarchar(max)",
                nullable: true);
            migration.AlterColumn(
                name: "parent_work_id",
                table: "work_links",
                type: "int",
                nullable: false);
            migration.AlterColumn(
                name: "group_index",
                table: "work_links",
                type: "int",
                nullable: false);
            migration.AlterColumn(
                name: "year",
                table: "works",
                type: "int",
                nullable: false);
            migration.AlterColumn(
                name: "year",
                table: "translation_works",
                type: "int",
                nullable: false);
            migration.AlterColumn(
                name: "name",
                table: "languages",
                type: "nvarchar(max)",
                nullable: false);
            migration.AlterColumn(
                name: "name",
                table: "countries",
                type: "nvarchar(max)",
                nullable: false);
            migration.RenameColumn(
                name: "edition_work_id",
                table: "edition_works",
                newName: "id");
            migration.RenameColumn(
                name: "edition_translation_id",
                table: "edition_translations",
                newName: "translation_edition_id");
            migration.RenameColumn(
                name: "edition_serie_id",
                table: "edition_series",
                newName: "id");
            migration.RenameColumn(
                name: "edition_publisher_id",
                table: "edition_publishers",
                newName: "id");
        }
    }
}
