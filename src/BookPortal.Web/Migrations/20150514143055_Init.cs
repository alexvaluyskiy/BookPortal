using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace BookPortal.Web.Migrations
{
    public partial class Init : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "countries",
                columns: table => new
                {
                    country_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    name = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.country_id);
                });
            migration.CreateTable(
                name: "languages",
                columns: table => new
                {
                    language_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    name = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.language_id);
                });
            migration.CreateTable(
                name: "awards",
                columns: table => new
                {
                    award_closed = table.Column(type: "bit", nullable: false),
                    country_id = table.Column(type: "int", nullable: true),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    description_copyright = table.Column(type: "nvarchar(max)", nullable: true),
                    homepage = table.Column(type: "nvarchar(max)", nullable: true),
                    award_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    is_opened = table.Column(type: "bit", nullable: false),
                    language_id = table.Column(type: "int", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    notes = table.Column(type: "nvarchar(max)", nullable: true),
                    rusname = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_awards", x => x.award_id);
                    table.ForeignKey(
                        name: "FK_awards_countries_country_id",
                        columns: x => x.country_id,
                        referencedTable: "countries",
                        referencedColumn: "country_id");
                    table.ForeignKey(
                        name: "FK_awards_languages_language_id",
                        columns: x => x.language_id,
                        referencedTable: "languages",
                        referencedColumn: "language_id");
                });
            migration.CreateTable(
                name: "contests",
                columns: table => new
                {
                    award_id = table.Column(type: "int", nullable: false),
                    date = table.Column(type: "datetime2", nullable: true),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    contest_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    name = table.Column(type: "nvarchar(max)", nullable: false),
                    name_year = table.Column(type: "int", nullable: false),
                    number = table.Column(type: "int", nullable: false),
                    place = table.Column(type: "nvarchar(max)", nullable: true),
                    short_description = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contests", x => x.contest_id);
                    table.ForeignKey(
                        name: "FK_contests_awards_award_id",
                        columns: x => x.award_id,
                        referencedTable: "awards",
                        referencedColumn: "award_id");
                });
            migration.CreateTable(
                name: "nominations",
                columns: table => new
                {
                    award_id = table.Column(type: "int", nullable: false),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    nomination_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    name = table.Column(type: "nvarchar(max)", nullable: false),
                    number = table.Column(type: "int", nullable: false),
                    rusname = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nominations", x => x.nomination_id);
                    table.ForeignKey(
                        name: "FK_nominations_awards_award_id",
                        columns: x => x.award_id,
                        referencedTable: "awards",
                        referencedColumn: "award_id");
                });
            migration.CreateTable(
                name: "contest_works",
                columns: table => new
                {
                    ContestId = table.Column(type: "int", nullable: false),
                    contest_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    is_winner = table.Column(type: "bit", nullable: false),
                    link_id = table.Column(type: "int", nullable: true),
                    link_type = table.Column(type: "int", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    NominationId = table.Column(type: "int", nullable: false),
                    number = table.Column(type: "int", nullable: false),
                    postfix = table.Column(type: "nvarchar(max)", nullable: true),
                    prefix = table.Column(type: "nvarchar(max)", nullable: true),
                    rusname = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contest_works", x => x.contest_id);
                    table.ForeignKey(
                        name: "FK_contest_works_contests_ContestId",
                        columns: x => x.ContestId,
                        referencedTable: "contests",
                        referencedColumn: "contest_id");
                    table.ForeignKey(
                        name: "FK_contest_works_nominations_NominationId",
                        columns: x => x.NominationId,
                        referencedTable: "nominations",
                        referencedColumn: "nomination_id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("awards");
            migration.DropTable("contests");
            migration.DropTable("contest_works");
            migration.DropTable("countries");
            migration.DropTable("languages");
            migration.DropTable("nominations");
        }
    }
}
