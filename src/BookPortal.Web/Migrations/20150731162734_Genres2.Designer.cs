using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using BookPortal.Web.Domain;

namespace BookPortalWebMigrations
{
    [ContextType(typeof(BookContext))]
    partial class Genres2
    {
        public override string Id
        {
            get { return "20150731162734_Genres2"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815")
                .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

            builder.Entity("BookPortal.Web.Domain.Models.Award", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "award_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("AwardId")
                        .Annotation("Relational:ColumnName", "award_id");

                    b.Property<string>("Date")
                        .Annotation("Relational:ColumnName", "date")
                        .Annotation("Relational:ColumnType", "nvarchar(10)");

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
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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

                    b.Index("LinkId", "LinkType");

                    b.Annotation("Relational:TableName", "contest_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "country_id");

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 50)
                        .Annotation("Relational:ColumnName", "name");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "countries");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Edition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "edition_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<string>("Authors")
                        .Annotation("Relational:ColumnName", "authors");

                    b.Property<string>("Compilers")
                        .Annotation("Relational:ColumnName", "compilers");

                    b.Property<string>("Content")
                        .Annotation("Relational:ColumnName", "content");

                    b.Property<int>("Correct")
                        .Annotation("Relational:ColumnName", "correct");

                    b.Property<int?>("Count")
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

                    b.Property<int?>("Pages")
                        .Annotation("Relational:ColumnName", "pages");

                    b.Property<string>("ReleaseDate")
                        .Annotation("Relational:ColumnName", "release_date");

                    b.Property<bool>("SuperCover")
                        .Annotation("Relational:ColumnName", "supercover");

                    b.Property<int>("Type")
                        .Annotation("Relational:ColumnName", "type");

                    b.Property<int?>("Year")
                        .Annotation("Relational:ColumnName", "year");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "editions");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionPublisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "edition_publisher_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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
                        .Annotation("Relational:ColumnName", "edition_serie_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("EditionId")
                        .Annotation("Relational:ColumnName", "edition_id");

                    b.Property<int>("SerieId")
                        .Annotation("Relational:ColumnName", "serie_id");

                    b.Property<int>("Sort")
                        .Annotation("Relational:ColumnName", "sort");

                    b.Key("Id");

                    b.Index("SerieId");

                    b.Annotation("Relational:TableName", "edition_series");
                });

            builder.Entity("BookPortal.Web.Domain.Models.EditionTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "edition_translation_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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
                        .Annotation("Relational:ColumnName", "edition_work_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("EditionId")
                        .Annotation("Relational:ColumnName", "edition_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Index("WorkId");

                    b.Annotation("Relational:TableName", "edition_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.GenrePersonView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "genre_person_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("GenreCount")
                        .Annotation("Relational:ColumnName", "genre_count");

                    b.Property<int>("GenreTotal")
                        .Annotation("Relational:ColumnName", "genre_total");

                    b.Property<int>("PersonId")
                        .Annotation("Relational:ColumnName", "person_id");

                    b.Property<int>("WorkGenreId")
                        .Annotation("Relational:ColumnName", "work_genre_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "genre_persons_view");
                });

            builder.Entity("BookPortal.Web.Domain.Models.GenreWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "genre_work_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<string>("Description")
                        .Annotation("Relational:ColumnName", "description");

                    b.Property<int>("GenreWorkGroupId")
                        .Annotation("Relational:ColumnName", "genre_work_group_id");

                    b.Property<int>("Level")
                        .Annotation("Relational:ColumnName", "level");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int>("ParentGenreWorkId")
                        .Annotation("Relational:ColumnName", "parent_genre_work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "genre_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.GenreWorkUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "genre_work_user_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<DateTime>("DateCreated")
                        .Annotation("Relational:ColumnName", "date_created");

                    b.Property<int>("GenreWorkId")
                        .Annotation("Relational:ColumnName", "genre_work_id");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "genre_work_users");
                });

            builder.Entity("BookPortal.Web.Domain.Models.GenreWorkView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "genre_work_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("GenreCount")
                        .Annotation("Relational:ColumnName", "genre_count");

                    b.Property<int>("WorkGenreId")
                        .Annotation("Relational:ColumnName", "work_genre_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "genre_works_view");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "language_id");

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 50)
                        .Annotation("Relational:ColumnName", "name");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "languages");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "mark_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("MarkValue")
                        .Annotation("Relational:ColumnName", "mark_value");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Index("WorkId");

                    b.Index("WorkId", "UserId");

                    b.Annotation("Relational:TableName", "marks");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Nomination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "nomination_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<string>("Biography")
                        .Annotation("Relational:ColumnName", "biography");

                    b.Property<string>("BiographySource")
                        .Annotation("Relational:ColumnName", "biography_source");

                    b.Property<string>("Birthdate")
                        .Annotation("Relational:ColumnName", "birthdate")
                        .Annotation("Relational:ColumnType", "nvarchar(10)");

                    b.Property<int?>("CountryId")
                        .Annotation("Relational:ColumnName", "country_id");

                    b.Property<string>("Deathdate")
                        .Annotation("Relational:ColumnName", "deathdate")
                        .Annotation("Relational:ColumnType", "nvarchar(10)");

                    b.Property<int>("Gender")
                        .Annotation("Relational:ColumnName", "gender");

                    b.Property<bool>("IsOpened")
                        .Annotation("Relational:ColumnName", "is_opened");

                    b.Property<int?>("LanguageId")
                        .Annotation("Relational:ColumnName", "default_language_id");

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
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("PersonId")
                        .Annotation("Relational:ColumnName", "person_id");

                    b.Property<int>("Type")
                        .Annotation("Relational:ColumnName", "type");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Index("PersonId");

                    b.Annotation("Relational:TableName", "person_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "publisher_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("PublisherId")
                        .Annotation("Relational:ColumnName", "publisher_id");

                    b.Property<int>("SerieId")
                        .Annotation("Relational:ColumnName", "serie_id");

                    b.Key("Id");

                    b.Index("PublisherId");

                    b.Annotation("Relational:TableName", "publisher_series");
                });

            builder.Entity("BookPortal.Web.Domain.Models.RatingAuthorView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "rating_author_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("MarksCount")
                        .Annotation("Relational:ColumnName", "marks_count");

                    b.Property<int>("MarksWeight")
                        .Annotation("Relational:ColumnName", "marks_weight");

                    b.Property<int>("PersonId")
                        .Annotation("Relational:ColumnName", "person_id");

                    b.Property<double>("Rating")
                        .Annotation("Relational:ColumnName", "rating");

                    b.Property<int>("UsersCount")
                        .Annotation("Relational:ColumnName", "users_count");

                    b.Key("Id");

                    b.Index("PersonId")
                        .Unique();

                    b.Annotation("Relational:TableName", "rating_author_view");
                });

            builder.Entity("BookPortal.Web.Domain.Models.RatingWorkExpectView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "rating_work_expect_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("ExpectType")
                        .Annotation("Relational:ColumnName", "expect_type");

                    b.Property<string>("PlanDate")
                        .Annotation("Relational:ColumnName", "plan_date");

                    b.Property<int>("UsersCount")
                        .Annotation("Relational:ColumnName", "users_count");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "rating_work_expect_view");
                });

            builder.Entity("BookPortal.Web.Domain.Models.RatingWorkView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "rating_work_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("MarksCount")
                        .Annotation("Relational:ColumnName", "marks_count");

                    b.Property<double>("Rating")
                        .Annotation("Relational:ColumnName", "rating");

                    b.Property<string>("RatingType")
                        .Annotation("MaxLength", 50)
                        .Annotation("Relational:ColumnName", "rating_type");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Index("WorkId", "RatingType")
                        .Unique();

                    b.Annotation("Relational:TableName", "rating_work_view");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "review_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<DateTime>("DateCreated")
                        .Annotation("Relational:ColumnName", "date_created");

                    b.Property<string>("Text")
                        .Annotation("Relational:ColumnName", "text");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Index("UserId");

                    b.Index("WorkId");

                    b.Annotation("Relational:TableName", "reviews");
                });

            builder.Entity("BookPortal.Web.Domain.Models.ReviewVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "reviews_vote_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<DateTime>("DateCreated")
                        .Annotation("Relational:ColumnName", "date_created");

                    b.Property<int>("ReviewId")
                        .Annotation("Relational:ColumnName", "review_id");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Property<int>("Vote")
                        .Annotation("Relational:ColumnName", "vote");

                    b.Key("Id");

                    b.Index("ReviewId");

                    b.Annotation("Relational:TableName", "review_votes");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Serie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "serie_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("LanguageId")
                        .Annotation("Relational:ColumnName", "language_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Property<int?>("Year")
                        .Annotation("Relational:ColumnName", "year");

                    b.Key("Id");

                    b.Index("WorkId");

                    b.Annotation("Relational:TableName", "translation_works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.TranslationWorkPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "translation_work_person_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<int>("PersonId")
                        .Annotation("Relational:ColumnName", "person_id");

                    b.Property<int>("PersonOrder")
                        .Annotation("Relational:ColumnName", "person_order");

                    b.Property<int>("TranslationWorkId")
                        .Annotation("Relational:ColumnName", "translation_work_id");

                    b.Key("Id");

                    b.Index("PersonId");

                    b.Index("TranslationWorkId");

                    b.Annotation("Relational:TableName", "translation_work_persons");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "work_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

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

                    b.Property<int?>("Year")
                        .Annotation("Relational:ColumnName", "year");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "works");
                });

            builder.Entity("BookPortal.Web.Domain.Models.WorkLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "work_link_id")
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

                    b.Property<string>("BonusText")
                        .Annotation("Relational:ColumnName", "bonus_text");

                    b.Property<int?>("GroupIndex")
                        .Annotation("Relational:ColumnName", "group_index");

                    b.Property<bool>("IsAddition")
                        .Annotation("Relational:ColumnName", "is_addition");

                    b.Property<int>("LinkType")
                        .Annotation("Relational:ColumnName", "link_type");

                    b.Property<int?>("ParentWorkId")
                        .Annotation("Relational:ColumnName", "parent_work_id");

                    b.Property<int>("WorkId")
                        .Annotation("Relational:ColumnName", "work_id");

                    b.Key("Id");

                    b.Index("ParentWorkId");

                    b.Annotation("Relational:TableName", "work_links");
                });

            builder.Entity("BookPortal.Web.Domain.Models.WorkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "work_type_id");

                    b.Property<int>("Level")
                        .Annotation("Relational:ColumnName", "level");

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 50)
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("NameSingle")
                        .Annotation("MaxLength", 50)
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

            builder.Entity("BookPortal.Web.Domain.Models.GenrePersonView", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Person")
                        .InverseCollection()
                        .ForeignKey("PersonId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.GenreWorkUser", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.GenreWork")
                        .InverseCollection()
                        .ForeignKey("GenreWorkId");

                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.GenreWorkView", b =>
                {
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

            builder.Entity("BookPortal.Web.Domain.Models.RatingAuthorView", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Person")
                        .InverseCollection()
                        .ForeignKey("PersonId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.RatingWorkExpectView", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.RatingWorkView", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Review", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Work")
                        .InverseCollection()
                        .ForeignKey("WorkId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.ReviewVote", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Review")
                        .InverseCollection()
                        .ForeignKey("ReviewId");
                });

            builder.Entity("BookPortal.Web.Domain.Models.Serie", b =>
                {
                    b.Reference("BookPortal.Web.Domain.Models.Language")
                        .InverseCollection()
                        .ForeignKey("LanguageId");
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
