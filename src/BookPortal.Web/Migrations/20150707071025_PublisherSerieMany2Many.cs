using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class PublisherSerieMany2Many : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Serie_Publisher_PublisherId", table: "series");
            migration.DropColumn(name: "publisher_id", table: "series");
            migration.CreateTable(
                name: "publisher_series",
                columns: table => new
                {
                    publisher_serie_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    publisher_id = table.Column(type: "int", nullable: false),
                    serie_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherSerie", x => x.publisher_serie_id);
                    table.ForeignKey(
                        name: "FK_PublisherSerie_Publisher_PublisherId",
                        columns: x => x.publisher_id,
                        referencedTable: "publishers",
                        referencedColumn: "publisher_id");
                    table.ForeignKey(
                        name: "FK_PublisherSerie_Serie_SerieId",
                        columns: x => x.serie_id,
                        referencedTable: "series",
                        referencedColumn: "serie_id");
                });
            migration.AddColumn(
                name: "language_id",
                table: "series",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Serie_Language_LanguageId",
                table: "series",
                column: "language_id",
                referencedTable: "languages",
                referencedColumn: "language_id");
            migration.RenameColumn(
                name: "ReleaseDate",
                table: "editions",
                newName: "release_date");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Serie_Language_LanguageId", table: "series");
            migration.DropColumn(name: "language_id", table: "series");
            migration.DropTable("publisher_series");
            migration.AddColumn(
                name: "publisher_id",
                table: "series",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Serie_Publisher_PublisherId",
                table: "series",
                column: "publisher_id",
                referencedTable: "publishers",
                referencedColumn: "publisher_id");
            migration.RenameColumn(
                name: "release_date",
                table: "editions",
                newName: "ReleaseDate");
        }
    }
}
