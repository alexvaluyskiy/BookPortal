using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

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
                name: "Country",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });
            migration.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });
            migration.CreateTable(
                name: "Award",
                columns: table => new
                {
                    AwardType = table.Column(type: "int", nullable: false),
                    Comment = table.Column(type: "nvarchar(max)", nullable: true),
                    Compiler = table.Column(type: "nvarchar(max)", nullable: true),
                    Copyright = table.Column(type: "nvarchar(max)", nullable: true),
                    CopyrightLink = table.Column(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column(type: "int", nullable: true),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    Homepage = table.Column(type: "nvarchar(max)", nullable: true),
                    Id = table.Column(type: "int", nullable: false),
                    IsClosed = table.Column(type: "bit", nullable: false),
                    IsOpened = table.Column(type: "bit", nullable: false),
                    LanguageId = table.Column(type: "int", nullable: true),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column(type: "nvarchar(max)", nullable: true),
                    ProcessStatus = table.Column(type: "nvarchar(max)", nullable: true),
                    RusName = table.Column(type: "nvarchar(max)", nullable: true),
                    ShowInList = table.Column(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Award_Country_CountryId",
                        columns: x => x.CountryId,
                        referencedTable: "Country",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Award_Language_LanguageId",
                        columns: x => x.LanguageId,
                        referencedTable: "Language",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "Contest",
                columns: table => new
                {
                    AwardId = table.Column(type: "int", nullable: false),
                    Date = table.Column(type: "datetime2", nullable: true),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    Id = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    NameYear = table.Column(type: "int", nullable: false),
                    Number = table.Column(type: "int", nullable: false),
                    Place = table.Column(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contest_Award_AwardId",
                        columns: x => x.AwardId,
                        referencedTable: "Award",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "Nomination",
                columns: table => new
                {
                    AwardId = table.Column(type: "int", nullable: false),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    Id = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    Number = table.Column(type: "int", nullable: false),
                    RusName = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nomination_Award_AwardId",
                        columns: x => x.AwardId,
                        referencedTable: "Award",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "ContestWork",
                columns: table => new
                {
                    ContestId = table.Column(type: "int", nullable: false),
                    Id = table.Column(type: "int", nullable: false),
                    IsWinner = table.Column(type: "bit", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    NominationId = table.Column(type: "int", nullable: false),
                    Number = table.Column(type: "int", nullable: false),
                    Postfix = table.Column(type: "nvarchar(max)", nullable: true),
                    Prefix = table.Column(type: "nvarchar(max)", nullable: true),
                    RusName = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContestWork_Contest_ContestId",
                        columns: x => x.ContestId,
                        referencedTable: "Contest",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContestWork_Nomination_NominationId",
                        columns: x => x.NominationId,
                        referencedTable: "Nomination",
                        referencedColumn: "Id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("Award");
            migration.DropTable("Contest");
            migration.DropTable("ContestWork");
            migration.DropTable("Country");
            migration.DropTable("Language");
            migration.DropTable("Nomination");
        }
    }
}
