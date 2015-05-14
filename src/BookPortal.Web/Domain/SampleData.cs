using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.Web.Domain
{
    public static class SampleData
    {
        public static async Task InitializeMusicStoreDatabaseAsync(IServiceProvider serviceProvider)
        {
            await InsertTestData(serviceProvider);
        }

        private static async Task InsertTestData(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<BookContext>())
            {
                foreach (var country in GetContries())
                {
                    db.Countries.Add(country);
                }
                await db.SaveChangesAsync();

                foreach (var language in GetLanguages())
                {
                    db.Languages.Add(language);
                }
                await db.SaveChangesAsync();

                foreach (var award in GetAwards())
                {
                    db.Awards.Add(award);
                }
                await db.SaveChangesAsync();

                foreach (var nomination in GetNominations())
                {
                    db.Nominations.Add(nomination);
                }
                await db.SaveChangesAsync();

                foreach (var contest in GetContests())
                {
                    db.Contests.Add(contest);
                }
                await db.SaveChangesAsync();

                foreach (var contestWork in GetContestsWorks())
                {
                    db.ContestsWorks.Add(contestWork);
                }
                await db.SaveChangesAsync();

                foreach (var person in GetPersons())
                {
                    db.Persons.Add(person);
                }
                await db.SaveChangesAsync();

                foreach (var work in GetWorks())
                {
                    db.Works.Add(work);
                }
                await db.SaveChangesAsync();
            }
        }

        private static IEnumerable<Country> GetContries()
        {
            yield return new Country { Name = "Россия" };
            yield return new Country { Name = "США" };
            yield return new Country { Name = "Украина" };
        }

        private static IEnumerable<Language> GetLanguages()
        {
            yield return new Language { Name = "русский" };
            yield return new Language { Name = "английский" };
            yield return new Language { Name = "украинский" };
        }

        private static IEnumerable<Award> GetAwards()
        {
            yield return new Award
            {
                RusName = "Хьюго",
                Name = "Hugo",
                Homepage = "http://www.thehugoawards.org/",
                AwardClosed = false,
                Description = "Премия основана в 1960 году",
                DescriptionCopyright = "(с) fantmagaz",
                Notes = "Самая известная премия",
                IsOpened = true,
                CountryId = 2,
                LanguageId = 2
            };

            yield return new Award { RusName = "Небьюла", Name = "Nebula", CountryId = 2, LanguageId = 2 };
        }

        private static IEnumerable<Nomination> GetNominations()
        {
            yield return new Nomination
            {
                AwardId = 1,
                RusName = "Роман",
                Name = "Best Novel",
                Number = 1,
                Description = "за романы"
            };

            yield return new Nomination
            {
                AwardId = 1,
                RusName = "Повесть",
                Name = "Best Novella",
                Number = 2,
                Description = "за повести"
            };
        }

        private static IEnumerable<Contest> GetContests()
        {
            yield return new Contest
            {
                AwardId = 1,
                Name = "1953",
                Date = new DateTime(1953, 8, 16),
            };

            yield return new Contest
            {
                AwardId = 1,
                Name = "2014",
                NameYear = 2014,
                Date = new DateTime(2014, 8, 16),
                Place = "LonCon 3, Лондон",
                ShortDescription = "Конвент проходил с 14 по 18 августа"
            };

            yield return new Contest
            {
                AwardId = 2,
                Name = "1992",
                Date = new DateTime(1992, 8, 16),
            };

            yield return new Contest
            {
                AwardId = 2,
                Name = "2015",
                Date = new DateTime(2015, 8, 16),
            };
        }

        private static IEnumerable<ContestWork> GetContestsWorks()
        {
            yield return new ContestWork()
            {
                ContestId = 1,
                NominationId = 1,
                RusName = "Энн Леки - Слуги правосудия",
                Name = "Ann Leckie - Ancillary Justice",
                Number = 1,
                IsWinner = true,
                LinkType = ContestWorkType.Work,
                LinkId = 483569
            };
        }

        private static IEnumerable<Person> GetPersons()
        {
            yield return new Person
            {
                Name = "Ден Симмонс",
                NameOriginal = "Дена Симмонс",
                Biography = "биография",
                Gender = GenderType.Male,
                Birthdate = new DateTime(1931, 9, 9)
            };

            yield return new Person
            {
                Name = "Лев Толстой",
                NameOriginal = "Льва Толстого",
                Biography = "биография",
                Gender = GenderType.Male,
                Birthdate = new DateTime(1828, 9, 9),
                Deathdate = new DateTime(1910, 11, 20)
            };
        }

        private static IEnumerable<Work> GetWorks()
        {
            yield return new Work
            {
                Name = "Война и мир",
                Year = 1866
            };
        }
    }
}
