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
                name: "publishers",
                columns: table => new
                {
                    CountryId = table.Column(type: "int", nullable: false),
                    DateClose = table.Column(type: "datetime2", nullable: true),
                    DateOpen = table.Column(type: "datetime2", nullable: true),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    DescriptionSource = table.Column(type: "nvarchar(max)", nullable: true),
                    publisher_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    Type = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.publisher_id);
                    table.ForeignKey(
                        name: "FK_publishers_countries_CountryId",
                        columns: x => x.CountryId,
                        referencedTable: "countries",
                        referencedColumn: "country_id");
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
                name: "persons",
                columns: table => new
                {
                    Biography = table.Column(type: "nvarchar(max)", nullable: true),
                    BiographySource = table.Column(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column(type: "datetime2", nullable: true),
                    CountryId = table.Column(type: "int", nullable: false),
                    Deathdate = table.Column(type: "datetime2", nullable: true),
                    Gender = table.Column(type: "int", nullable: false),
                    person_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    LanguageId = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    NameOriginal = table.Column(type: "nvarchar(max)", nullable: true),
                    NameRp = table.Column(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.person_id);
                    table.ForeignKey(
                        name: "FK_persons_countries_CountryId",
                        columns: x => x.CountryId,
                        referencedTable: "countries",
                        referencedColumn: "country_id");
                    table.ForeignKey(
                        name: "FK_persons_languages_LanguageId",
                        columns: x => x.LanguageId,
                        referencedTable: "languages",
                        referencedColumn: "language_id");
                });
            migration.CreateTable(
                name: "series",
                columns: table => new
                {
                    DateClose = table.Column(type: "datetime2", nullable: true),
                    DateOpen = table.Column(type: "datetime2", nullable: true),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    serie_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    PublisherId = table.Column(type: "int", nullable: false),
                    SerieClosed = table.Column(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_series", x => x.serie_id);
                    table.ForeignKey(
                        name: "FK_series_publishers_PublisherId",
                        columns: x => x.PublisherId,
                        referencedTable: "publishers",
                        referencedColumn: "publisher_id");
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
                name: "works",
                columns: table => new
                {
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    PersonId = table.Column(type: "int", nullable: false),
                    Year = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.work_id);
                    table.ForeignKey(
                        name: "FK_works_persons_PersonId",
                        columns: x => x.PersonId,
                        referencedTable: "persons",
                        referencedColumn: "person_id");
                });
            migration.CreateTable(
                name: "contest_works",
                columns: table => new
                {
                    contest_id = table.Column(type: "int", nullable: false),
                    contest_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    is_winner = table.Column(type: "bit", nullable: false),
                    link_id = table.Column(type: "int", nullable: true),
                    link_type = table.Column(type: "int", nullable: false),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    nomination_id = table.Column(type: "int", nullable: false),
                    number = table.Column(type: "int", nullable: false),
                    postfix = table.Column(type: "nvarchar(max)", nullable: true),
                    prefix = table.Column(type: "nvarchar(max)", nullable: true),
                    rusname = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contest_works", x => x.contest_work_id);
                    table.ForeignKey(
                        name: "FK_contest_works_contests_contest_id",
                        columns: x => x.contest_id,
                        referencedTable: "contests",
                        referencedColumn: "contest_id");
                    table.ForeignKey(
                        name: "FK_contest_works_nominations_nomination_id",
                        columns: x => x.nomination_id,
                        referencedTable: "nominations",
                        referencedColumn: "nomination_id");
                });
            migration.CreateTable(
                name: "editions",
                columns: table => new
                {
                    Authors = table.Column(type: "nvarchar(max)", nullable: true),
                    Count = table.Column(type: "int", nullable: false),
                    Format = table.Column(type: "nvarchar(max)", nullable: true),
                    edition_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    Page = table.Column(type: "int", nullable: false),
                    Publishers = table.Column(type: "nvarchar(max)", nullable: true),
                    Series = table.Column(type: "nvarchar(max)", nullable: true),
                    WorkId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_editions", x => x.edition_id);
                    table.ForeignKey(
                        name: "FK_editions_works_WorkId",
                        columns: x => x.WorkId,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "translation_works",
                columns: table => new
                {
                    translation_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    WorkId = table.Column(type: "int", nullable: false),
                    Year = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_translation_works", x => x.translation_work_id);
                    table.ForeignKey(
                        name: "FK_translation_works_works_WorkId",
                        columns: x => x.WorkId,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "translation_editions",
                columns: table => new
                {
                    EditionId = table.Column(type: "int", nullable: false),
                    translation_editions = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    TranslationWorkId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_translation_editions", x => x.translation_editions);
                    table.ForeignKey(
                        name: "FK_translation_editions_editions_EditionId",
                        columns: x => x.EditionId,
                        referencedTable: "editions",
                        referencedColumn: "edition_id");
                    table.ForeignKey(
                        name: "FK_translation_editions_translation_works_TranslationWorkId",
                        columns: x => x.TranslationWorkId,
                        referencedTable: "translation_works",
                        referencedColumn: "translation_work_id");
                });
            migration.CreateTable(
                name: "translation_work_persons",
                columns: table => new
                {
                    translation_work_person_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    PersonId = table.Column(type: "int", nullable: false),
                    TranslationWorkId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_translation_work_persons", x => x.translation_work_person_id);
                    table.ForeignKey(
                        name: "FK_translation_work_persons_persons_PersonId",
                        columns: x => x.PersonId,
                        referencedTable: "persons",
                        referencedColumn: "person_id");
                    table.ForeignKey(
                        name: "FK_translation_work_persons_translation_works_TranslationWorkId",
                        columns: x => x.TranslationWorkId,
                        referencedTable: "translation_works",
                        referencedColumn: "translation_work_id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("awards");
            migration.DropTable("contests");
            migration.DropTable("contest_works");
            migration.DropTable("countries");
            migration.DropTable("editions");
            migration.DropTable("languages");
            migration.DropTable("nominations");
            migration.DropTable("persons");
            migration.DropTable("publishers");
            migration.DropTable("series");
            migration.DropTable("translation_editions");
            migration.DropTable("translation_works");
            migration.DropTable("translation_work_persons");
            migration.DropTable("works");
        }
    }
}
