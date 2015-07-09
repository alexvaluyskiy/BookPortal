using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using BookPortal.Web.Domain;

namespace BookPortalWebMigrations
{
    [ContextType(typeof(BookContext))]
    partial class YearOpenCloseSeries
    {
        public override string Id
        {
            get { return "20150709092514_YearOpenCloseSeries"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13698"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13698")
                .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

            builder.Entity("BookPortal.Web.Domain.Models.Award", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "award_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<bool>("AwardClosed")
                        .Annotation("Relational:ColumnName", "award_closed");

                    b.Property<int?>("CountryId")
                        .Annotation("Relational:ColumnName", "country_id");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<string>("DescriptionSource")
                        .Annotation("Relational:ColumnName", "description_source");

                    b.Property<string>("Homepage")
                        .Annotation("Relational:ColumnName", "homepage");

                    b.Property<bool>("IsOpened")
                        .Annotation("Relational:ColumnName", "is_opened");

                    b.Property<int?>("LanguageId")
                        .Annotation("Relational:ColumnName", "language_id");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("Notes")
                        .Annotation("Relational:ColumnName", "notes");

                    b.Property<string>("RusName")
                        .Required()
                        .Annotation("Relational:ColumnName", "rusname");

                    b.Key("Id");

                    b.Index("IsOpened");

                    b.Annotation("Relational:TableName", "awards");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "contest_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("AwardId")
                        .Annotation("Relational:ColumnName", "award_id");

                    b.Property<DateTime>("Date")
                        .Annotation("Relational:ColumnName", "date");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int>("NameYear")
                        .Annotation("Relational:ColumnName", "name_year");

                    b.Property<int>("Number")
                        .Annotation("Relational:ColumnName", "number");

                    b.Property<string>("Place")
                        .Annotation("Relational:ColumnName", "place");

                    b.Property<string>("ShortDescription")
                        .Annotation("Relational:ColumnName", "short_description");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "contests");
                });

            builder.Entity("BookPortal.Web.Domain.Models.ContestWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "contest_work_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("ContestId")
                        .Annotation("Relational:ColumnName", "contest_id");

                    b.Property<bool>("IsWinner")
                        .Annotation("Relational:ColumnName", "is_winner");

                    b.Property<int?>("LinkId")
                        .Annotation("Relational:ColumnName", "link_id");

                    b.Property<int>("LinkType")
                        .Annotation("Relational:ColumnName", "link_type");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int>("NominationId")
                        .Annotation("Relational:ColumnName", "nomination_id");

                    b.Property<int>("Number")
                        .Annotation("Relational:ColumnName", "number");

                    b.Property<string>("Postfix")
                        .Annotation("Relational:ColumnName", "postfix");

                    b.Property<string>("Prefix")
                        .Annotation("Relational:ColumnName", "prefix");

                    b.Property<string>("RusName")
                        .Annotation("Relational:ColumnName", "rusname");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "contest_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "country_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("Relational:ColumnName", "name");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "countries");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Edition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "edition_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("Authors")
                        .Annotation("Relational:ColumnName", "authors");

                    b.Property<string>("Compilers")
                        .Annotation("Relational:ColumnName", "compilers");

                    b.Property<string>("Content")
                        .Annotation("Relational:ColumnName", "content");

                    b.Property<int>("Count")
                        .Annotation("Relational:ColumnName", "count");

                    b.Property<int>("CoverType")
                        .Annotation("Relational:ColumnName", "cover_type");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<string>("Format")
                        .Annotation("Relational:ColumnName", "format");

                    b.Property<string>("Isbn")
                        .Annotation("Relational:ColumnName", "isbn");

                    b.Property<int?>("LanguageId")
                        .Annotation("Relational:ColumnName", "language_id");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("Notes")
                        .Annotation("Relational:ColumnName", "notes");

                    b.Property<int>("Pages")
                        .Annotation("Relational:ColumnName", "pages");

                    b.Property<DateTime?>("ReleaseDate")
                        .Annotation("Relational:ColumnName", "release_date");

                    b.Property<bool>("SuperCover")
                        .Annotation("Relational:ColumnName", "supercover");

                    b.Property<int>("Type")
                        .Annotation("Relational:ColumnName", "type");

                    b.Property<int>("Year")
                        .Annotation("Relational:ColumnName", "year");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "editions");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionPublisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("EditionId")
                        .Annotation("Relational:ColumnName", "edition_id");

                    b.Property<int>("PublisherId")
                        .Annotation("Relational:ColumnName", "publisher_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "edition_publishers");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionSerie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("EditionId")
                        .Annotation("Relational:ColumnName", "edition_id");

                    b.Property<int>("SerieId")
                        .Annotation("Relational:ColumnName", "serie_id");

                    b.Property<int>("Sort")
                        .Annotation("Relational:ColumnName", "sort");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "edition_series");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "translation_edition_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("EditionId")
                        .Annotation("Relational:ColumnName", "edition_id");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int>("TranslationWorkId")
                        .Annotation("Relational:ColumnName", "translation_work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "edition_translations");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("EditionId")
                        .Annotation("Relational:ColumnName", "edition_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "edition_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "language_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("Relational:ColumnName", "name");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "languages");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Nomination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "nomination_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("AwardId")
                        .Annotation("Relational:ColumnName", "award_id");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int>("Number")
                        .Annotation("Relational:ColumnName", "number");

                    b.Property<string>("RusName")
                        .Annotation("Relational:ColumnName", "rusname");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "nominations");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "person_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("Biography")
                        .Annotation("Relational:ColumnName", "biography");

                    b.Property<string>("BiographySource")
                        .Annotation("Relational:ColumnName", "biography_source");

                    b.Property<DateTime?>("Birthdate")
                        .Annotation("Relational:ColumnName", "birthdate");

                    b.Property<int?>("CountryId")
                        .Annotation("Relational:ColumnName", "country_id");

                    b.Property<DateTime?>("Deathdate")
                        .Annotation("Relational:ColumnName", "deathdate");

                    b.Property<int>("Gender")
                        .Annotation("Relational:ColumnName", "gender");

                    b.Property<int?>("LanguageId")
                        .Annotation("Relational:ColumnName", "language_id");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("NameOriginal")
                        .Annotation("Relational:ColumnName", "name_original");

                    b.Property<string>("NameRp")
                        .Annotation("Relational:ColumnName", "name_rp");

                    b.Property<string>("NameSort")
                        .Annotation("Relational:ColumnName", "name_sort");

                    b.Property<string>("Notes")
                        .Annotation("Relational:ColumnName", "notes");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "persons");
                });

            builder.Entity("BookPortal.Web.Domain.Models.PersonWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "person_work_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("PersonId")
                        .Annotation("Relational:ColumnName", "person_id");

                    b.Property<int>("Type")
                        .Annotation("Relational:ColumnName", "type");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "person_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "publisher_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int?>("CountryId")
                        .Annotation("Relational:ColumnName", "country_id");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<string>("DescriptionSource")
                        .Annotation("Relational:ColumnName", "description_source");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int>("Type")
                        .Annotation("Relational:ColumnName", "type");

                    b.Property<int?>("YearClose")
                        .Annotation("Relational:ColumnName", "year_close");

                    b.Property<int?>("YearOpen")
                        .Annotation("Relational:ColumnName", "year_open");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "publishers");
                });

            builder.Entity("BookPortal.Web.Domain.Models.PublisherSerie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "publisher_serie_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("PublisherId")
                        .Annotation("Relational:ColumnName", "publisher_id");

                    b.Property<int>("SerieId")
                        .Annotation("Relational:ColumnName", "serie_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "publisher_series");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Serie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "serie_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<int?>("LanguageId")
                        .Annotation("Relational:ColumnName", "language_id");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int?>("ParentSerieId")
                        .Annotation("Relational:ColumnName", "parent_serie_id");

                    b.Property<bool>("SerieClosed")
                        .Annotation("Relational:ColumnName", "serie_closed");

                    b.Property<int?>("YearClose")
                        .Annotation("Relational:ColumnName", "year_close");

                    b.Property<int?>("YearOpen")
                        .Annotation("Relational:ColumnName", "year_open");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "series");
                });

            builder.Entity("BookPortal.Web.Domain.Models.TranslationWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "translation_work_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("LanguageId")
                        .Annotation("Relational:ColumnName", "language_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Property<int>("Year")
                        .Annotation("Relational:ColumnName", "year");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "translation_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.TranslationWorkPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "translation_work_person_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("PersonId")
                        .Annotation("Relational:ColumnName", "person_id");

                    b.Property<int>("PersonOrder")
                        .Annotation("Relational:ColumnName", "person_order");

                    b.Property<int>("TranslationWorkId")
                        .Annotation("Relational:ColumnName", "translation_work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "translation_work_persons");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "work_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("AltName")
                        .Annotation("Relational:ColumnName", "altname");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("RusName")
                        .Annotation("Relational:ColumnName", "rusname");

                    b.Property<int>("WorkTypeId")
                        .Annotation("Relational:ColumnName", "work_type_id");

                    b.Property<int>("Year")
                        .Annotation("Relational:ColumnName", "year");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.WorkLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "work_link_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("BonusText")
                        .Annotation("Relational:ColumnName", "bonus_text");

                    b.Property<int>("GroupIndex")
                        .Annotation("Relational:ColumnName", "group_index");

                    b.Property<bool>("IsAddition")
                        .Annotation("Relational:ColumnName", "is_addition");

                    b.Property<int>("LinkType")
                        .Annotation("Relational:ColumnName", "link_type");

                    b.Property<int>("ParentWorkId")
                        .Annotation("Relational:ColumnName", "parent_work_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "work_links");
                });

            builder.Entity("BookPortal.Web.Domain.Models.WorkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "work_type_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<int>("Level")
                        .Annotation("Relational:ColumnName", "level");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("NameSingle")
                        .Annotation("Relational:ColumnName", "name_single");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "work_types");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Award", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Country")
                        .InverseCollection()
                        .ForeignKey("CountryId");

                    b.Reference("BookPortal.Web.Domain.Models.Language")
                        .InverseCollection()
                        .ForeignKey("LanguageId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Contest", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Award")
                        .InverseCollection()
                        .ForeignKey("AwardId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.ContestWork", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Contest")
                        .InverseCollection()
                        .ForeignKey("ContestId");

                    b.Reference("BookPortal.Web.Domain.Models.Nomination")
                        .InverseCollection()
                        .ForeignKey("NominationId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Edition", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Language")
                        .InverseCollection()
                        .ForeignKey("LanguageId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionPublisher", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Edition")
                        .InverseCollection()
                        .ForeignKey("EditionId");

                    b.Reference("BookPortal.Web.Domain.Models.Publisher")
                        .InverseCollection()
                        .ForeignKey("PublisherId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionSerie", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Edition")
                        .InverseCollection()
                        .ForeignKey("EditionId");

                    b.Reference("BookPortal.Web.Domain.Models.Serie")
                        .InverseCollection()
                        .ForeignKey("SerieId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionTranslation", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Edition")
                        .InverseCollection()
                        .ForeignKey("EditionId");

                    b.Reference("BookPortal.Web.Domain.Models.TranslationWork")
                        .InverseCollection()
                        .ForeignKey("TranslationWorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionWork", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Edition")
                        .InverseCollection()
                        .ForeignKey("EditionId");

                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Nomination", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Award")
                        .InverseCollection()
                        .ForeignKey("AwardId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Person", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Country")
                        .InverseCollection()
                        .ForeignKey("CountryId");

                    b.Reference("BookPortal.Web.Domain.Models.Language")
                        .InverseCollection()
                        .ForeignKey("LanguageId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.PersonWork", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Person")
                        .InverseCollection()
                        .ForeignKey("PersonId");

                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Publisher", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Country")
                        .InverseCollection()
                        .ForeignKey("CountryId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.PublisherSerie", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Publisher")
                        .InverseCollection()
                        .ForeignKey("PublisherId");

                    b.Reference("BookPortal.Web.Domain.Models.Serie")
                        .InverseCollection()
                        .ForeignKey("SerieId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Serie", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Language")
                        .InverseCollection()
                        .ForeignKey("LanguageId");

                    b.Reference("BookPortal.Web.Domain.Models.Serie")
                        .InverseCollection()
                        .ForeignKey("ParentSerieId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.TranslationWork", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Language")
                        .InverseCollection()
                        .ForeignKey("LanguageId");

                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.TranslationWorkPerson", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Person")
                        .InverseCollection()
                        .ForeignKey("PersonId");

                    b.Reference("BookPortal.Web.Domain.Models.TranslationWork")
                        .InverseCollection()
                        .ForeignKey("TranslationWorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Work", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.WorkType")
                        .InverseCollection()
                        .ForeignKey("WorkTypeId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.WorkLink", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("ParentWorkId");

                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });
        }
    }
}
