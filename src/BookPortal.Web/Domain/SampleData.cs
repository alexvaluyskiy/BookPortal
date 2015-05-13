﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Domain.Models;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.Web.Domain
{
    public static class SampleData
    {
        public static async Task InitializeMusicStoreDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<BookContext>())
            {
                foreach (var country in GetContries())
                {
                    db.Countries.Add(country);
                }

                foreach (var language in GetLanguages())
                {
                    db.Languages.Add(language);
                }

                foreach (var award in GetAwards())
                {
                    db.Awards.Add(award);
                }

                foreach (var nomination in GetNominations())
                {
                    db.Nominations.Add(nomination);
                }

                foreach (var contest in GetContests())
                {
                    db.Contests.Add(contest);
                }

                foreach (var contestWork in GetContestsWorks())
                {
                    db.ContestsWorks.Add(contestWork);
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
    }
}