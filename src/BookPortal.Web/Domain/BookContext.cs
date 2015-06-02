using BookPortal.Web.Domain.Models;
using Microsoft.Data.Entity;

namespace BookPortal.Web.Domain
{
    public class BookContext : DbContext
    {
        public DbSet<Award> Awards { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Nomination> Nominations { get; set; }
        public DbSet<ContestWork> ContestWorks { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonWork> PersonWorks { get; set; }

        public DbSet<Work> Works { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        public DbSet<TranslationWork> TranslationWorks { get; set; }
        public DbSet<TranslationWorkPerson> TranslationWorkPersons { get; set; }
        
        public DbSet<Edition> Editions { get; set; }
        public DbSet<EditionPublisher> EditionPublishers { get; set; }
        public DbSet<EditionSerie> EditionSeries { get; set; }
        public DbSet<EditionTranslation> TranslationEditions { get; set; }
        public DbSet<EditionWork> EditionWorks { get; set; }

        public DbSet<Serie> Series { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Award>().ForSqlServer().Table("awards");
            modelBuilder.Entity<Award>().Index(c => c.IsOpened);
            modelBuilder.Entity<Award>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("award_id");
                builder.Property(c => c.RusName).Required().ForSqlServer().Column("rusname");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.Homepage).ForSqlServer().Column("homepage");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.DescriptionSource).ForSqlServer().Column("description_source");
                builder.Property(c => c.Notes).ForSqlServer().Column("notes");
                builder.Property(c => c.AwardClosed).ForSqlServer().Column("award_closed");
                builder.Property(c => c.IsOpened).ForSqlServer().Column("is_opened");
                builder.Property(c => c.LanguageId).ForSqlServer().Column("language_id");
                builder.Property(c => c.CountryId).ForSqlServer().Column("country_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Contest>().ForSqlServer().Table("contests");
            modelBuilder.Entity<Contest>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("contest_id");
                builder.Property(c => c.Name).Required().ForSqlServer().Column("name");
                builder.Property(c => c.NameYear).ForSqlServer().Column("name_year");
                builder.Property(c => c.Number).ForSqlServer().Column("number");
                builder.Property(c => c.Place).ForSqlServer().Column("place");
                builder.Property(c => c.Date).ForSqlServer().Column("date");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.ShortDescription).ForSqlServer().Column("short_description");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.AwardId).ForSqlServer().Column("award_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Nomination>().ForSqlServer().Table("nominations");
            modelBuilder.Entity<Nomination>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("nomination_id");
                builder.Property(c => c.Name).Required().ForSqlServer().Column("name");
                builder.Property(c => c.RusName).ForSqlServer().Column("rusname");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.Number).ForSqlServer().Column("number");
                builder.Property(c => c.AwardId).ForSqlServer().Column("award_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<ContestWork>().ForSqlServer().Table("contest_works");
            modelBuilder.Entity<ContestWork>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("contest_work_id");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.RusName).ForSqlServer().Column("rusname");
                builder.Property(c => c.Prefix).ForSqlServer().Column("prefix");
                builder.Property(c => c.Postfix).ForSqlServer().Column("postfix");
                builder.Property(c => c.Number).ForSqlServer().Column("number");
                builder.Property(c => c.IsWinner).ForSqlServer().Column("is_winner");
                builder.Property(c => c.LinkType).ForSqlServer().Column("link_type");
                builder.Property(c => c.LinkId).ForSqlServer().Column("link_id");
                builder.Property(c => c.NominationId).ForSqlServer().Column("nomination_id");
                builder.Property(c => c.ContestId).ForSqlServer().Column("contest_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Person>().ForSqlServer().Table("persons");
            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("person_id");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.NameRp).ForSqlServer().Column("name_rp");
                builder.Property(c => c.NameOriginal).ForSqlServer().Column("name_original");
                builder.Property(c => c.NameSort).ForSqlServer().Column("name_sort");
                builder.Property(c => c.Gender).ForSqlServer().Column("gender");
                builder.Property(c => c.Birthdate).ForSqlServer().Column("bidthdate");
                builder.Property(c => c.Deathdate).ForSqlServer().Column("deathdate");
                builder.Property(c => c.CountryId).ForSqlServer().Column("country_id");
                builder.Property(c => c.LanguageId).ForSqlServer().Column("language_id");
                builder.Property(c => c.Biography).ForSqlServer().Column("biography");
                builder.Property(c => c.BiographySource).ForSqlServer().Column("biography_source");
                builder.Property(c => c.Notes).ForSqlServer().Column("notes");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<PersonWork>().ForSqlServer().Table("person_works");
            modelBuilder.Entity<PersonWork>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("id");
                builder.Property(c => c.Type).ForSqlServer().Column("type");
                builder.Property(c => c.PersonId).ForSqlServer().Column("person_id");
                builder.Property(c => c.WorkId).ForSqlServer().Column("work_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Work>().ForSqlServer().Table("works");
            modelBuilder.Entity<Work>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("work_id");
                builder.Property(c => c.RusName).ForSqlServer().Column("rusname");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.AltName).ForSqlServer().Column("altname");
                builder.Property(c => c.Year).ForSqlServer().Column("year");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.WorkTypeId).ForSqlServer().Column("work_type_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<WorkType>().ForSqlServer().Table("work_types");
            modelBuilder.Entity<WorkType>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("work_type_id");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.NameSingle).ForSqlServer().Column("name_single");
                builder.Property(c => c.Level).ForSqlServer().Column("level");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<TranslationWork>().ForSqlServer().Table("translation_works");
            modelBuilder.Entity<TranslationWork>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("translation_work_id");
                builder.Property(c => c.Year).ForSqlServer().Column("year");
                builder.Property(c => c.WorkId).ForSqlServer().Column("work_id");
                builder.Property(c => c.LanguageId).ForSqlServer().Column("language_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<TranslationWorkPerson>().ForSqlServer().Table("translation_work_persons");
            modelBuilder.Entity<TranslationWorkPerson>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("translation_work_person_id");
                builder.Property(c => c.PersonId).ForSqlServer().Column("person_id");
                builder.Property(c => c.TranslationWorkId).ForSqlServer().Column("translation_work_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Edition>().ForSqlServer().Table("editions");
            modelBuilder.Entity<Edition>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("edition_id");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.Type).ForSqlServer().Column("type");
                builder.Property(c => c.Authors).ForSqlServer().Column("authors");
                builder.Property(c => c.Compilers).ForSqlServer().Column("compilers");
                builder.Property(c => c.Isbn).ForSqlServer().Column("isbn");
                builder.Property(c => c.Year).ForSqlServer().Column("year");
                builder.Property(c => c.Count).ForSqlServer().Column("count");
                builder.Property(c => c.CoverType).ForSqlServer().Column("cover_type");
                builder.Property(c => c.SuperCover).ForSqlServer().Column("supercover");
                builder.Property(c => c.Format).ForSqlServer().Column("format");
                builder.Property(c => c.Pages).ForSqlServer().Column("pages");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.Content).ForSqlServer().Column("content");
                builder.Property(c => c.Notes).ForSqlServer().Column("notes");
                builder.Property(c => c.LanguageId).ForSqlServer().Column("language_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<EditionPublisher>().ForSqlServer().Table("edition_publishers");
            modelBuilder.Entity<EditionPublisher>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("id");
                builder.Property(c => c.EditionId).ForSqlServer().Column("edition_id");
                builder.Property(c => c.PublisherId).ForSqlServer().Column("publisher_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<EditionSerie>().ForSqlServer().Table("edition_series");
            modelBuilder.Entity<EditionSerie>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("id");
                builder.Property(c => c.Sort).ForSqlServer().Column("sort");
                builder.Property(c => c.EditionId).ForSqlServer().Column("edition_id");
                builder.Property(c => c.SerieId).ForSqlServer().Column("serie_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<EditionTranslation>().ForSqlServer().Table("edition_translations");
            modelBuilder.Entity<EditionTranslation>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("translation_edition_id");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.TranslationWorkId).ForSqlServer().Column("translation_work_id");
                builder.Property(c => c.EditionId).ForSqlServer().Column("edition_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<EditionWork>().ForSqlServer().Table("edition_works");
            modelBuilder.Entity<EditionWork>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("id");
                builder.Property(c => c.EditionId).ForSqlServer().Column("edition_id");
                builder.Property(c => c.WorkId).ForSqlServer().Column("work_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Serie>().ForSqlServer().Table("series");
            modelBuilder.Entity<Serie>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("serie_id");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.DateOpen).ForSqlServer().Column("date_open");
                builder.Property(c => c.DateClose).ForSqlServer().Column("date_close");
                builder.Property(c => c.SerieClosed).ForSqlServer().Column("serie_closed");
                builder.Property(c => c.ParentSerieId).ForSqlServer().Column("parent_serie_id");
                builder.Property(c => c.PublisherId).ForSqlServer().Column("publisher_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Publisher>().ForSqlServer().Table("publishers");
            modelBuilder.Entity<Publisher>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("publisher_id");
                builder.Property(c => c.Name).ForSqlServer().Column("name");
                builder.Property(c => c.Type).ForSqlServer().Column("type");
                builder.Property(c => c.DateOpen).ForSqlServer().Column("date_open");
                builder.Property(c => c.DateClose).ForSqlServer().Column("date_close");
                builder.Property(c => c.Description).ForSqlServer().Column("description");
                builder.Property(c => c.DescriptionSource).ForSqlServer().Column("description_source");
                builder.Property(c => c.CountryId).ForSqlServer().Column("country_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Country>().ForSqlServer().Table("countries");
            modelBuilder.Entity<Country>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("country_id");
                builder.Property(c => c.Name).Required().ForSqlServer().Column("name");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Language>().ForSqlServer().Table("languages");
            modelBuilder.Entity<Language>(builder =>
            {
                builder.Property(c => c.Id).ForSqlServer().Column("language_id");
                builder.Property(c => c.Name).Required().ForSqlServer().Column("name");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
