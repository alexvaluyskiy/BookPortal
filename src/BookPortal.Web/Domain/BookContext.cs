using System;
using System.Linq;
using BookPortal.Web.Domain.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace BookPortal.Web.Domain
{
    public class BookContext : DbContext
    {
        public DbSet<Award> Awards { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Nomination> Nominations { get; set; }
        public DbSet<ContestWork> ContestsWorks { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Publisher> Publisher { get; set; }

        public DbSet<TranslationWork> TranslationWorks { get; set; }
        public DbSet<TranslationWorkPerson> TranslationWorkPersons { get; set; }
        public DbSet<TranslationEdition> TranslationEditions { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Award>().ForRelational().Table("awards");
            modelBuilder.Entity<Award>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("award_id");
                builder.Property(c => c.RusName).Required().ForRelational().Column("rusname");
                builder.Property(c => c.Name).ForRelational().Column("name");
                builder.Property(c => c.Homepage).ForRelational().Column("homepage");
                builder.Property(c => c.Description).ForRelational().Column("description");
                builder.Property(c => c.DescriptionCopyright).ForRelational().Column("description_copyright");
                builder.Property(c => c.Notes).ForRelational().Column("notes");
                builder.Property(c => c.AwardClosed).ForRelational().Column("award_closed");
                builder.Property(c => c.IsOpened).ForRelational().Column("is_opened");
                builder.Property(c => c.LanguageId).ForRelational().Column("language_id");
                builder.Property(c => c.CountryId).ForRelational().Column("country_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Contest>().ForRelational().Table("contests");
            modelBuilder.Entity<Contest>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("contest_id");
                builder.Property(c => c.Name).Required().ForRelational().Column("name");
                builder.Property(c => c.NameYear).ForRelational().Column("name_year");
                builder.Property(c => c.Number).ForRelational().Column("number");
                builder.Property(c => c.Place).ForRelational().Column("place");
                builder.Property(c => c.Date).ForRelational().Column("date");
                builder.Property(c => c.Description).ForRelational().Column("description");
                builder.Property(c => c.ShortDescription).ForRelational().Column("short_description");
                builder.Property(c => c.Description).ForRelational().Column("description");
                builder.Property(c => c.AwardId).ForRelational().Column("award_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Nomination>().ForRelational().Table("nominations");
            modelBuilder.Entity<Nomination>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("nomination_id");
                builder.Property(c => c.Name).Required().ForRelational().Column("name");
                builder.Property(c => c.RusName).ForRelational().Column("rusname");
                builder.Property(c => c.Description).ForRelational().Column("description");
                builder.Property(c => c.Number).ForRelational().Column("number");
                builder.Property(c => c.AwardId).ForRelational().Column("award_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<ContestWork>().ForRelational().Table("contest_works");
            modelBuilder.Entity<ContestWork>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("contest_work_id");
                builder.Property(c => c.Name).ForRelational().Column("name");
                builder.Property(c => c.RusName).ForRelational().Column("rusname");
                builder.Property(c => c.Prefix).ForRelational().Column("prefix");
                builder.Property(c => c.Postfix).ForRelational().Column("postfix");
                builder.Property(c => c.Number).ForRelational().Column("number");
                builder.Property(c => c.IsWinner).ForRelational().Column("is_winner");
                builder.Property(c => c.LinkType).ForRelational().Column("link_type");
                builder.Property(c => c.LinkId).ForRelational().Column("link_id");
                builder.Property(c => c.NominationId).ForRelational().Column("nomination_id");
                builder.Property(c => c.ContestId).ForRelational().Column("contest_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Country>().ForRelational().Table("countries");
            modelBuilder.Entity<Country>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("country_id");
                builder.Property(c => c.Name).Required().ForRelational().Column("name");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Language>().ForRelational().Table("languages");
            modelBuilder.Entity<Language>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("language_id");
                builder.Property(c => c.Name).Required().ForRelational().Column("name");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Person>().ForRelational().Table("persons");
            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("person_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Work>().ForRelational().Table("works");
            modelBuilder.Entity<Work>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("work_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Edition>().ForRelational().Table("editions");
            modelBuilder.Entity<Edition>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("edition_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Serie>().ForRelational().Table("series");
            modelBuilder.Entity<Serie>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("serie_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<Publisher>().ForRelational().Table("publishers");
            modelBuilder.Entity<Publisher>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("publisher_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<TranslationWork>().ForRelational().Table("translation_works");
            modelBuilder.Entity<TranslationWork>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("translation_work_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<TranslationWorkPerson>().ForRelational().Table("translation_work_persons");
            modelBuilder.Entity<TranslationWorkPerson>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("translation_work_person_id");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            modelBuilder.Entity<TranslationEdition>().ForRelational().Table("translation_editions");
            modelBuilder.Entity<TranslationEdition>(builder =>
            {
                builder.Property(c => c.Id).ForRelational().Column("translation_editions");

                builder.Property(c => c.Id).ForSqlServer().UseIdentity();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
