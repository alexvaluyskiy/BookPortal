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
        public DbSet<WorkLink> WorkLinks { get; set; }
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
        public DbSet<PublisherSerie> PublisherSeries { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Award>().ToTable("awards");
            modelBuilder.Entity<Award>().Index(c => c.IsOpened);
            modelBuilder.Entity<Award>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("award_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.RusName).Required().HasColumnName("rusname");
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.Homepage).HasColumnName("homepage");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.DescriptionSource).HasColumnName("description_source");
                builder.Property(c => c.Notes).HasColumnName("notes");
                builder.Property(c => c.AwardClosed).HasColumnName("award_closed");
                builder.Property(c => c.IsOpened).HasColumnName("is_opened");
                builder.Property(c => c.LanguageId).HasColumnName("language_id");
                builder.Property(c => c.CountryId).HasColumnName("country_id");
            });

            modelBuilder.Entity<Contest>().ToTable("contests");
            modelBuilder.Entity<Contest>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("contest_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).Required().HasColumnName("name");
                builder.Property(c => c.NameYear).HasColumnName("name_year");
                builder.Property(c => c.Number).HasColumnName("number");
                builder.Property(c => c.Place).HasColumnName("place");
                builder.Property(c => c.Date).HasColumnName("date");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.ShortDescription).HasColumnName("short_description");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.AwardId).HasColumnName("award_id");
            });

            modelBuilder.Entity<Nomination>().ToTable("nominations");
            modelBuilder.Entity<Nomination>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("nomination_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).Required().HasColumnName("name");
                builder.Property(c => c.RusName).HasColumnName("rusname");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.Number).HasColumnName("number");
                builder.Property(c => c.AwardId).HasColumnName("award_id");
            });

            modelBuilder.Entity<ContestWork>().ToTable("contest_works");
            modelBuilder.Entity<ContestWork>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("contest_work_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.RusName).HasColumnName("rusname");
                builder.Property(c => c.Prefix).HasColumnName("prefix");
                builder.Property(c => c.Postfix).HasColumnName("postfix");
                builder.Property(c => c.Number).HasColumnName("number");
                builder.Property(c => c.IsWinner).HasColumnName("is_winner");
                builder.Property(c => c.LinkType).HasColumnName("link_type");
                builder.Property(c => c.LinkId).HasColumnName("link_id");
                builder.Property(c => c.NominationId).HasColumnName("nomination_id");
                builder.Property(c => c.ContestId).HasColumnName("contest_id");
            });

            modelBuilder.Entity<Person>().ToTable("persons");
            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("person_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.NameRp).HasColumnName("name_rp");
                builder.Property(c => c.NameOriginal).HasColumnName("name_original");
                builder.Property(c => c.NameSort).HasColumnName("name_sort");
                builder.Property(c => c.Gender).HasColumnName("gender");
                builder.Property(c => c.Birthdate).HasColumnName("birthdate");
                builder.Property(c => c.Deathdate).HasColumnName("deathdate");
                builder.Property(c => c.CountryId).HasColumnName("country_id");
                builder.Property(c => c.LanguageId).HasColumnName("language_id");
                builder.Property(c => c.Biography).HasColumnName("biography");
                builder.Property(c => c.BiographySource).HasColumnName("biography_source");
                builder.Property(c => c.Notes).HasColumnName("notes");
            });

            modelBuilder.Entity<PersonWork>().ToTable("person_works");
            modelBuilder.Entity<PersonWork>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("person_work_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Type).HasColumnName("type");
                builder.Property(c => c.PersonId).HasColumnName("person_id");
                builder.Property(c => c.WorkId).HasColumnName("work_id");
            });

            modelBuilder.Entity<Work>().ToTable("works");
            modelBuilder.Entity<Work>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("work_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.RusName).HasColumnName("rusname");
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.AltName).HasColumnName("altname");
                builder.Property(c => c.Year).HasColumnName("year");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.WorkTypeId).HasColumnName("work_type_id");
            });

            modelBuilder.Entity<WorkLink>().ToTable("work_links");
            modelBuilder.Entity<WorkLink>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("work_link_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.WorkId).HasColumnName("work_id");
                builder.Property(c => c.ParentWorkId).HasColumnName("parent_work_id");
                builder.Property(c => c.LinkType).HasColumnName("link_type");
                builder.Property(c => c.IsAddition).HasColumnName("is_addition");
                builder.Property(c => c.BonusText).HasColumnName("bonus_text");
                builder.Property(c => c.GroupIndex).HasColumnName("group_index");
            });

            modelBuilder.Entity<WorkType>().ToTable("work_types");
            modelBuilder.Entity<WorkType>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("work_type_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.NameSingle).HasColumnName("name_single");
                builder.Property(c => c.Level).HasColumnName("level");
            });

            modelBuilder.Entity<TranslationWork>().ToTable("translation_works");
            modelBuilder.Entity<TranslationWork>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("translation_work_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Year).HasColumnName("year");
                builder.Property(c => c.WorkId).HasColumnName("work_id");
                builder.Property(c => c.LanguageId).HasColumnName("language_id");
            });

            modelBuilder.Entity<TranslationWorkPerson>().ToTable("translation_work_persons");
            modelBuilder.Entity<TranslationWorkPerson>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("translation_work_person_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.PersonId).HasColumnName("person_id");
                builder.Property(c => c.TranslationWorkId).HasColumnName("translation_work_id");
            });

            modelBuilder.Entity<Edition>().ToTable("editions");
            modelBuilder.Entity<Edition>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("edition_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.Type).HasColumnName("type");
                builder.Property(c => c.Authors).HasColumnName("authors");
                builder.Property(c => c.Compilers).HasColumnName("compilers");
                builder.Property(c => c.Isbn).HasColumnName("isbn");
                builder.Property(c => c.Year).HasColumnName("year");
                builder.Property(c => c.Count).HasColumnName("count");
                builder.Property(c => c.CoverType).HasColumnName("cover_type");
                builder.Property(c => c.SuperCover).HasColumnName("supercover");
                builder.Property(c => c.Format).HasColumnName("format");
                builder.Property(c => c.Pages).HasColumnName("pages");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.Content).HasColumnName("content");
                builder.Property(c => c.Notes).HasColumnName("notes");
                builder.Property(c => c.ReleaseDate).HasColumnName("release_date");
                builder.Property(c => c.LanguageId).HasColumnName("language_id");
            });

            modelBuilder.Entity<EditionPublisher>().ToTable("edition_publishers");
            modelBuilder.Entity<EditionPublisher>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("id").UseSqlServerIdentityColumn();
                builder.Property(c => c.EditionId).HasColumnName("edition_id");
                builder.Property(c => c.PublisherId).HasColumnName("publisher_id");
            });

            modelBuilder.Entity<EditionSerie>().ToTable("edition_series");
            modelBuilder.Entity<EditionSerie>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Sort).HasColumnName("sort");
                builder.Property(c => c.EditionId).HasColumnName("edition_id");
                builder.Property(c => c.SerieId).HasColumnName("serie_id");
            });

            modelBuilder.Entity<EditionTranslation>().ToTable("edition_translations");
            modelBuilder.Entity<EditionTranslation>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("translation_edition_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.TranslationWorkId).HasColumnName("translation_work_id");
                builder.Property(c => c.EditionId).HasColumnName("edition_id");
            });

            modelBuilder.Entity<EditionWork>().ToTable("edition_works");
            modelBuilder.Entity<EditionWork>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("id").UseSqlServerIdentityColumn();
                builder.Property(c => c.EditionId).HasColumnName("edition_id");
                builder.Property(c => c.WorkId).HasColumnName("work_id");
            });

            modelBuilder.Entity<Serie>().ToTable("series");
            modelBuilder.Entity<Serie>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("serie_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.DateOpen).HasColumnName("date_open");
                builder.Property(c => c.DateClose).HasColumnName("date_close");
                builder.Property(c => c.SerieClosed).HasColumnName("serie_closed");
                builder.Property(c => c.ParentSerieId).HasColumnName("parent_serie_id");
                builder.Property(c => c.LanguageId).HasColumnName("language_id");
            });

            modelBuilder.Entity<Publisher>().ToTable("publishers");
            modelBuilder.Entity<Publisher>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("publisher_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).HasColumnName("name");
                builder.Property(c => c.Type).HasColumnName("type");
                builder.Property(c => c.DateOpen).HasColumnName("date_open");
                builder.Property(c => c.DateClose).HasColumnName("date_close");
                builder.Property(c => c.Description).HasColumnName("description");
                builder.Property(c => c.DescriptionSource).HasColumnName("description_source");
                builder.Property(c => c.CountryId).HasColumnName("country_id");
            });

            modelBuilder.Entity<PublisherSerie>().ToTable("publisher_series");
            modelBuilder.Entity<PublisherSerie>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("publisher_serie_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.PublisherId).HasColumnName("publisher_id");
                builder.Property(c => c.SerieId).HasColumnName("serie_id");
            });

            modelBuilder.Entity<Country>().ToTable("countries");
            modelBuilder.Entity<Country>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("country_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).Required().HasColumnName("name");
            });

            modelBuilder.Entity<Language>().ToTable("languages");
            modelBuilder.Entity<Language>(builder =>
            {
                builder.Property(c => c.Id).HasColumnName("language_id").UseSqlServerIdentityColumn();
                builder.Property(c => c.Name).Required().HasColumnName("name");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
