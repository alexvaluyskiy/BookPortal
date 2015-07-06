using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace BookPortalWebMigrations
{
    public partial class Migration1 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "countries",
                columns: table => new
                {
                    country_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    name = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.country_id);
                });
            migration.CreateTable(
                name: "languages",
                columns: table => new
                {
                    language_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    name = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.language_id);
                });
            migration.CreateTable(
                name: "work_types",
                columns: table => new
                {
                    work_type_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    level = table.Column(type: "int", nullable: false),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    name_single = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkType", x => x.work_type_id);
                });
            migration.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    publisher_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    country_id = table.Column(type: "int", nullable: true),
                    date_close = table.Column(type: "datetime2", nullable: true),
                    date_open = table.Column(type: "datetime2", nullable: true),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    description_source = table.Column(type: "nvarchar(max)", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    @type = table.Column(name: "type", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.publisher_id);
                    table.ForeignKey(
                        name: "FK_Publisher_Country_CountryId",
                        columns: x => x.country_id,
                        referencedTable: "countries",
                        referencedColumn: "country_id");
                });
            migration.CreateTable(
                name: "awards",
                columns: table => new
                {
                    award_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    award_closed = table.Column(type: "bit", nullable: false),
                    country_id = table.Column(type: "int", nullable: true),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    description_source = table.Column(type: "nvarchar(max)", nullable: true),
                    homepage = table.Column(type: "nvarchar(max)", nullable: true),
                    is_opened = table.Column(type: "bit", nullable: false),
                    language_id = table.Column(type: "int", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    notes = table.Column(type: "nvarchar(max)", nullable: true),
                    rusname = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.award_id);
                    table.ForeignKey(
                        name: "FK_Award_Country_CountryId",
                        columns: x => x.country_id,
                        referencedTable: "countries",
                        referencedColumn: "country_id");
                    table.ForeignKey(
                        name: "FK_Award_Language_LanguageId",
                        columns: x => x.language_id,
                        referencedTable: "languages",
                        referencedColumn: "language_id");
                });
            migration.CreateTable(
                name: "editions",
                columns: table => new
                {
                    edition_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    authors = table.Column(type: "nvarchar(max)", nullable: true),
                    compilers = table.Column(type: "nvarchar(max)", nullable: true),
                    content = table.Column(type: "nvarchar(max)", nullable: true),
                    count = table.Column(type: "int", nullable: false),
                    cover_type = table.Column(type: "int", nullable: false),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    format = table.Column(type: "nvarchar(max)", nullable: true),
                    isbn = table.Column(type: "nvarchar(max)", nullable: true),
                    language_id = table.Column(type: "int", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    notes = table.Column(type: "nvarchar(max)", nullable: true),
                    pages = table.Column(type: "int", nullable: false),
                    ReleaseDate = table.Column(type: "datetime2", nullable: true),
                    supercover = table.Column(type: "bit", nullable: false),
                    @type = table.Column(name: "type", type: "int", nullable: false),
                    year = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edition", x => x.edition_id);
                    table.ForeignKey(
                        name: "FK_Edition_Language_LanguageId",
                        columns: x => x.language_id,
                        referencedTable: "languages",
                        referencedColumn: "language_id");
                });
            migration.CreateTable(
                name: "persons",
                columns: table => new
                {
                    person_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    biography = table.Column(type: "nvarchar(max)", nullable: true),
                    biography_source = table.Column(type: "nvarchar(max)", nullable: true),
                    birthdate = table.Column(type: "datetime2", nullable: true),
                    country_id = table.Column(type: "int", nullable: true),
                    deathdate = table.Column(type: "datetime2", nullable: true),
                    gender = table.Column(type: "int", nullable: false),
                    language_id = table.Column(type: "int", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    name_original = table.Column(type: "nvarchar(max)", nullable: true),
                    name_rp = table.Column(type: "nvarchar(max)", nullable: true),
                    name_sort = table.Column(type: "nvarchar(max)", nullable: true),
                    notes = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.person_id);
                    table.ForeignKey(
                        name: "FK_Person_Country_CountryId",
                        columns: x => x.country_id,
                        referencedTable: "countries",
                        referencedColumn: "country_id");
                    table.ForeignKey(
                        name: "FK_Person_Language_LanguageId",
                        columns: x => x.language_id,
                        referencedTable: "languages",
                        referencedColumn: "language_id");
                });
            migration.CreateTable(
                name: "works",
                columns: table => new
                {
                    work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    altname = table.Column(type: "nvarchar(max)", nullable: true),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    rusname = table.Column(type: "nvarchar(max)", nullable: true),
                    work_type_id = table.Column(type: "int", nullable: false),
                    year = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.work_id);
                    table.ForeignKey(
                        name: "FK_Work_WorkType_WorkTypeId",
                        columns: x => x.work_type_id,
                        referencedTable: "work_types",
                        referencedColumn: "work_type_id");
                });
            migration.CreateTable(
                name: "series",
                columns: table => new
                {
                    serie_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    date_close = table.Column(type: "datetime2", nullable: true),
                    date_open = table.Column(type: "datetime2", nullable: true),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    parent_serie_id = table.Column(type: "int", nullable: true),
                    publisher_id = table.Column(type: "int", nullable: true),
                    serie_closed = table.Column(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serie", x => x.serie_id);
                    table.ForeignKey(
                        name: "FK_Serie_Serie_ParentSerieId",
                        columns: x => x.parent_serie_id,
                        referencedTable: "series",
                        referencedColumn: "serie_id");
                    table.ForeignKey(
                        name: "FK_Serie_Publisher_PublisherId",
                        columns: x => x.publisher_id,
                        referencedTable: "publishers",
                        referencedColumn: "publisher_id");
                });
            migration.CreateTable(
                name: "contests",
                columns: table => new
                {
                    contest_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    award_id = table.Column(type: "int", nullable: false),
                    date = table.Column(type: "datetime2", nullable: false),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: false),
                    name_year = table.Column(type: "int", nullable: false),
                    number = table.Column(type: "int", nullable: false),
                    place = table.Column(type: "nvarchar(max)", nullable: true),
                    short_description = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contest", x => x.contest_id);
                    table.ForeignKey(
                        name: "FK_Contest_Award_AwardId",
                        columns: x => x.award_id,
                        referencedTable: "awards",
                        referencedColumn: "award_id");
                });
            migration.CreateTable(
                name: "nominations",
                columns: table => new
                {
                    nomination_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    award_id = table.Column(type: "int", nullable: false),
                    description = table.Column(type: "nvarchar(max)", nullable: true),
                    name = table.Column(type: "nvarchar(max)", nullable: false),
                    number = table.Column(type: "int", nullable: false),
                    rusname = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomination", x => x.nomination_id);
                    table.ForeignKey(
                        name: "FK_Nomination_Award_AwardId",
                        columns: x => x.award_id,
                        referencedTable: "awards",
                        referencedColumn: "award_id");
                });
            migration.CreateTable(
                name: "edition_publishers",
                columns: table => new
                {
                    id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    edition_id = table.Column(type: "int", nullable: false),
                    publisher_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionPublisher", x => x.id);
                    table.ForeignKey(
                        name: "FK_EditionPublisher_Edition_EditionId",
                        columns: x => x.edition_id,
                        referencedTable: "editions",
                        referencedColumn: "edition_id");
                    table.ForeignKey(
                        name: "FK_EditionPublisher_Publisher_PublisherId",
                        columns: x => x.publisher_id,
                        referencedTable: "publishers",
                        referencedColumn: "publisher_id");
                });
            migration.CreateTable(
                name: "edition_works",
                columns: table => new
                {
                    id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    edition_id = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionWork", x => x.id);
                    table.ForeignKey(
                        name: "FK_EditionWork_Edition_EditionId",
                        columns: x => x.edition_id,
                        referencedTable: "editions",
                        referencedColumn: "edition_id");
                    table.ForeignKey(
                        name: "FK_EditionWork_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "person_works",
                columns: table => new
                {
                    person_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    person_id = table.Column(type: "int", nullable: false),
                    @type = table.Column(name: "type", type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonWork", x => x.person_work_id);
                    table.ForeignKey(
                        name: "FK_PersonWork_Person_PersonId",
                        columns: x => x.person_id,
                        referencedTable: "persons",
                        referencedColumn: "person_id");
                    table.ForeignKey(
                        name: "FK_PersonWork_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "translation_works",
                columns: table => new
                {
                    translation_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    language_id = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false),
                    year = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationWork", x => x.translation_work_id);
                    table.ForeignKey(
                        name: "FK_TranslationWork_Language_LanguageId",
                        columns: x => x.language_id,
                        referencedTable: "languages",
                        referencedColumn: "language_id");
                    table.ForeignKey(
                        name: "FK_TranslationWork_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "work_links",
                columns: table => new
                {
                    work_link_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    bonus_text = table.Column(type: "nvarchar(max)", nullable: true),
                    group_index = table.Column(type: "int", nullable: false),
                    is_addition = table.Column(type: "bit", nullable: false),
                    link_type = table.Column(type: "int", nullable: false),
                    parent_work_id = table.Column(type: "int", nullable: false),
                    work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLink", x => x.work_link_id);
                    table.ForeignKey(
                        name: "FK_WorkLink_Work_ParentWorkId",
                        columns: x => x.parent_work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                    table.ForeignKey(
                        name: "FK_WorkLink_Work_WorkId",
                        columns: x => x.work_id,
                        referencedTable: "works",
                        referencedColumn: "work_id");
                });
            migration.CreateTable(
                name: "edition_series",
                columns: table => new
                {
                    id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    edition_id = table.Column(type: "int", nullable: false),
                    serie_id = table.Column(type: "int", nullable: false),
                    sort = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionSerie", x => x.id);
                    table.ForeignKey(
                        name: "FK_EditionSerie_Edition_EditionId",
                        columns: x => x.edition_id,
                        referencedTable: "editions",
                        referencedColumn: "edition_id");
                    table.ForeignKey(
                        name: "FK_EditionSerie_Serie_SerieId",
                        columns: x => x.serie_id,
                        referencedTable: "series",
                        referencedColumn: "serie_id");
                });
            migration.CreateTable(
                name: "contest_works",
                columns: table => new
                {
                    contest_work_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    contest_id = table.Column(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ContestWork", x => x.contest_work_id);
                    table.ForeignKey(
                        name: "FK_ContestWork_Contest_ContestId",
                        columns: x => x.contest_id,
                        referencedTable: "contests",
                        referencedColumn: "contest_id");
                    table.ForeignKey(
                        name: "FK_ContestWork_Nomination_NominationId",
                        columns: x => x.nomination_id,
                        referencedTable: "nominations",
                        referencedColumn: "nomination_id");
                });
            migration.CreateTable(
                name: "edition_translations",
                columns: table => new
                {
                    translation_edition_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    edition_id = table.Column(type: "int", nullable: false),
                    name = table.Column(type: "nvarchar(max)", nullable: true),
                    translation_work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionTranslation", x => x.translation_edition_id);
                    table.ForeignKey(
                        name: "FK_EditionTranslation_Edition_EditionId",
                        columns: x => x.edition_id,
                        referencedTable: "editions",
                        referencedColumn: "edition_id");
                    table.ForeignKey(
                        name: "FK_EditionTranslation_TranslationWork_TranslationWorkId",
                        columns: x => x.translation_work_id,
                        referencedTable: "translation_works",
                        referencedColumn: "translation_work_id");
                });
            migration.CreateTable(
                name: "translation_work_persons",
                columns: table => new
                {
                    translation_work_person_id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn"),
                    person_id = table.Column(type: "int", nullable: false),
                    translation_work_id = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationWorkPerson", x => x.translation_work_person_id);
                    table.ForeignKey(
                        name: "FK_TranslationWorkPerson_Person_PersonId",
                        columns: x => x.person_id,
                        referencedTable: "persons",
                        referencedColumn: "person_id");
                    table.ForeignKey(
                        name: "FK_TranslationWorkPerson_TranslationWork_TranslationWorkId",
                        columns: x => x.translation_work_id,
                        referencedTable: "translation_works",
                        referencedColumn: "translation_work_id");
                });
            migration.CreateIndex(
                name: "IX_Award_IsOpened",
                table: "awards",
                column: "is_opened");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("contest_works");
            migration.DropTable("edition_publishers");
            migration.DropTable("edition_series");
            migration.DropTable("edition_translations");
            migration.DropTable("edition_works");
            migration.DropTable("person_works");
            migration.DropTable("translation_work_persons");
            migration.DropTable("work_links");
            migration.DropTable("contests");
            migration.DropTable("nominations");
            migration.DropTable("series");
            migration.DropTable("editions");
            migration.DropTable("persons");
            migration.DropTable("translation_works");
            migration.DropTable("awards");
            migration.DropTable("publishers");
            migration.DropTable("works");
            migration.DropTable("languages");
            migration.DropTable("countries");
            migration.DropTable("work_types");
        }
    }
}
