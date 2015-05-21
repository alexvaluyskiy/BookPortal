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
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<BookContext>())
            {
                var sqlServerDatabase = db.Database as SqlServerDatabase;
                if (sqlServerDatabase != null)
                {
                    await sqlServerDatabase.EnsureDeletedAsync();
                    await sqlServerDatabase.EnsureCreatedAsync();
                }
                await InsertTestData(serviceProvider);
            }
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
                    db.ContestWorks.Add(contestWork);
                }
                await db.SaveChangesAsync();

                foreach (var person in GetPersons())
                {
                    db.Persons.Add(person);
                }
                await db.SaveChangesAsync();

                foreach (var work in GetWorksTypes())
                {
                    db.WorkTypes.Add(work);
                }
                await db.SaveChangesAsync();

                foreach (var work in GetWorks())
                {
                    db.Works.Add(work);
                }
                await db.SaveChangesAsync();

                foreach (var work in GetPersonWorks())
                {
                    db.PersonWorks.Add(work);
                }
                await db.SaveChangesAsync();

                foreach (var edition in GetEditions())
                {
                    db.Editions.Add(edition);
                }
                await db.SaveChangesAsync();

                foreach (var edition in GetEditionsWorks())
                {
                    db.EditionWorks.Add(edition);
                }
                await db.SaveChangesAsync();

                foreach (var work in GetTranslationWorks())
                {
                    db.TranslationWorks.Add(work);
                }
                await db.SaveChangesAsync();

                foreach (var work in GetTranslationWorksPersons())
                {
                    db.TranslationWorkPersons.Add(work);
                }
                await db.SaveChangesAsync();

                foreach (var edition in GetTranslationEditions())
                {
                    db.TranslationEditions.Add(edition);
                }
                await db.SaveChangesAsync();

                foreach (var publisher in GetPublishers())
                {
                    db.Publishers.Add(publisher);
                }
                await db.SaveChangesAsync();

                foreach (var serie in GetSeries())
                {
                    db.Series.Add(serie);
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
                DescriptionSource = "(с) fantmagaz",
                Notes = "Самая известная премия",
                IsOpened = true,
                CountryId = 2,
                LanguageId = 2
            };

            yield return new Award
            {
                RusName = "Небьюла",
                Name = "Nebula",
                CountryId = 2,
                LanguageId = 2
            };
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
                NameSort = "Симмонса, Дена",
                Biography = "биография",
                Gender = GenderType.Male,
                Birthdate = new DateTime(1931, 9, 9),
                CountryId = 2,
                LanguageId = 2
            };

            yield return new Person
            {

                Name = "Лев Толстой",
                NameOriginal = "Льва Толстого",
                NameSort = "Толстого Льва",
                Biography = "биография",
                Gender = GenderType.Male,
                Birthdate = new DateTime(1828, 9, 9),
                Deathdate = new DateTime(1910, 11, 20),
                CountryId = 1,
                LanguageId = 1
            };

            yield return new Person
            {
                Name = "А. Коротков",
                NameOriginal = "А. Короткова",
                NameSort = "Коротков, А.",
                Biography = "биография переводчика",
                Gender = GenderType.Male
            };
        }

        private static IEnumerable<WorkType> GetWorksTypes()
        {
            yield return new WorkType { Name = "Романы", NameSingle = "Роман", Level = 6 };
            yield return new WorkType { Name = "Повести", NameSingle = "Повесть", Level = 7 };
            yield return new WorkType { Name = "Рассказы", NameSingle = "Рассказ", Level = 8 };
        }

        private static IEnumerable<Work> GetWorks()
        {
            yield return new Work
            {
                RusName = "Анна Каренина",
                Year = 1866,
                WorkTypeId = 1
            };

            yield return new Work
            {
                RusName = "Гиперион",
                Name = "Hyperion",
                Year = 1989,
                WorkTypeId = 1
            };
        }

        private static IEnumerable<PersonWork> GetPersonWorks()
        {
            yield return new PersonWork { WorkId = 1, PersonId = 2, Type = WorkPersonType.Author };
            yield return new PersonWork { WorkId = 2, PersonId = 1, Type = WorkPersonType.Author };
            yield return new PersonWork { WorkId = 2, PersonId = 2, Type = WorkPersonType.Author };
        } 

        private static IEnumerable<Edition> GetEditions()
        {
            yield return new Edition
            {
                Name = "Гиперион",
                Year = 1994
            };

            yield return new Edition
            {
                Name = "Хиперион",
                Year = 1995
            };

            yield return new Edition
            {
                Name = "Гиперион",
                Year = 1998
            };
        }

        private static IEnumerable<EditionWork> GetEditionsWorks()
        {
            yield return new EditionWork
            {
                EditionId = 1,
                WorkId = 2
            };

            yield return new EditionWork
            {
                EditionId = 2,
                WorkId = 2
            };

            yield return new EditionWork
            {
                EditionId = 3,
                WorkId = 2
            };
        }

        private static IEnumerable<TranslationWork> GetTranslationWorks()
        {
            yield return new TranslationWork
            {
                LanguageId = 1,
                WorkId = 2,
                Year = 1995
            };
        }

        private static IEnumerable<TranslationWorkPerson> GetTranslationWorksPersons()
        {
            yield return new TranslationWorkPerson
            {
                PersonId = 3,
                TranslationWorkId = 1
            };
        }

        private static IEnumerable<EditionTranslation> GetTranslationEditions()
        {
            yield return new EditionTranslation
            {
                TranslationWorkId = 1,
                Name = "Гиперион",
                EditionId = 1
            };

            yield return new EditionTranslation
            {
                TranslationWorkId = 1,
                Name = "Хиперион",
                EditionId = 2
            };

            yield return new EditionTranslation
            {
                TranslationWorkId = 1,
                Name = "Гиперион",
                EditionId = 3
            };
        }

        private static IEnumerable<Publisher> GetPublishers()
        {
            yield return new Publisher { Name = "Аст" };
            yield return new Publisher { Name = "Эксмо" };
        }

        private static IEnumerable<Serie> GetSeries()
        {
            yield return new Serie
            {
                Name = "Отцы-Основатели",
                DateOpen = new DateTime(2003, 1, 1),
                PublisherId = 2
            };
        
            yield return new Serie
            {
                Name = "Отцы-Основатели. Легенды фантастики",
                DateOpen = new DateTime(2003, 1, 1),
                PublisherId = 2,
                ParentSerieId = 1
            };

            yield return new Serie
            {
                Name = "Отцы-Основатели. Русское пространство",
                DateOpen = new DateTime(2003, 1, 1),
                PublisherId = 2,
                ParentSerieId = 1
            };

            yield return new Serie
            {
                Name = "Весь Желязны",
                DateOpen = new DateTime(2003, 1, 1),
                DateClose = new DateTime(2009, 1, 1),
                PublisherId = 2,
                ParentSerieId = 2
            };

            yield return new Serie
            {
                Name = "Иван Ефремов",
                DateOpen = new DateTime(2007, 1, 1),
                DateClose = new DateTime(2007, 1, 1),
                PublisherId = 2,
                ParentSerieId = 3
            };
        }
    }
}
