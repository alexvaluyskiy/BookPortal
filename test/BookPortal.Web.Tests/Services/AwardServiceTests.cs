using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace BookPortal.Web.Tests.Services
{
    public class AwardServiceTests
    {
        private readonly IServiceProvider _serviceProvider;

        public AwardServiceTests()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<BookContext>(c => c.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task GetAwardsTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            AwardRequest request = new AwardRequest { Sort = AwardSort.Id };
            var response = await service.GetAwardsAsync(request);

            Assert.NotNull(response);
            Assert.Equal(3, response.Values.Count);
            Assert.Equal(1, response.Values[0].AwardId);
            Assert.Equal("Nebula", response.Values[0].Name);

            Assert.NotNull(response.Values[0].FirstContestDate);
            //Assert.Equal(1992, response[0].FirstContestDate.Value.Year);
            Assert.NotNull(response.Values[0].LastContestDate);
            //Assert.Equal(2015, response[0].LastContestDate.Value.Year);
        }

        [Fact(Skip = "strange behavior of MemoryStorage")]
        public async Task GetAwardsLimitTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            AwardRequest request = new AwardRequest { Sort = AwardSort.Id, Limit = 2 };
            var response = await service.GetAwardsAsync(request);

            Assert.NotNull(response);
            Assert.Equal(2, response.Values.Count);
            Assert.Equal(1, response.Values[0].AwardId);
            Assert.Equal("Nebula", response.Values[0].Name);
        }

        [Fact(Skip = "strange behavior of MemoryStorage")]
        public async Task GetAwardsLimitOffsetTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            AwardRequest request = new AwardRequest { Sort = AwardSort.Id, Limit = 1, Offset = 1 };
            var response = await service.GetAwardsAsync(request);

            Assert.NotNull(response);
            Assert.Equal(1, response.Values.Count);
            Assert.Equal(2, response.Values[0].AwardId);
            Assert.Equal("Hugo", response.Values[0].Name);
        }

        [Fact]
        public async Task GetAwardsSortRusnameTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            AwardRequest request = new AwardRequest { Sort = AwardSort.Rusname };
            var response = await service.GetAwardsAsync(request);

            Assert.NotNull(response);
            Assert.Equal(3, response.Values.Count);
            Assert.Equal("Nebula", response.Values[0].Name);
            Assert.Equal("Oscar", response.Values[1].Name);
            Assert.Equal("Hugo", response.Values[2].Name);
        }

        [Fact]
        public async Task GetAwardsSortLanguageTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            AwardRequest request = new AwardRequest { Sort = AwardSort.Language };
            var response = await service.GetAwardsAsync(request);

            Assert.NotNull(response);
            Assert.Equal(3, response.Values.Count);
            Assert.Equal("Oscar", response.Values[0].Name);
            Assert.Equal("Nebula", response.Values[1].Name);
            Assert.Equal("Hugo", response.Values[2].Name);
        }

        [Fact]
        public async Task GetAwardsSortCountryTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            AwardRequest request = new AwardRequest { Sort = AwardSort.Country };
            var response = await service.GetAwardsAsync(request);

            Assert.NotNull(response);
            Assert.Equal(3, response.Values.Count);
            Assert.Equal("Hugo", response.Values[0].Name);
            Assert.Equal("Nebula", response.Values[1].Name);
            Assert.Equal("Oscar", response.Values[2].Name);
        }

        [Fact]
        public async Task GetAwardTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            var award = await service.GetAwardAsync(1);

            Assert.NotNull(award);
            Assert.Equal(1, award.AwardId);
            Assert.Equal("Nebula", award.Name);
            Assert.NotNull(award.FirstContestDate);
            //Assert.Equal(1992, award.FirstContestDate.Value.Year);
            Assert.NotNull(award.LastContestDate);
            //Assert.Equal(2015, award.LastContestDate.Value.Year);
        }

        private async Task CreateSampleTask(BookContext dbContext)
        {
            var languages = new List<Language>
            {
                new Language { Id = 1, Name = "русский" },
                new Language { Id = 2, Name = "английский" }
            };
            dbContext.Languages.AddRange(languages);
            await dbContext.SaveChangesAsync();

            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "США" },
                new Country { Id = 2, Name = "Россия" }
            };
            dbContext.Countries.AddRange(countries);
            await dbContext.SaveChangesAsync();

            var awards = new List<Award>
            {
                new Award { Id = 1, Name = "Nebula", RusName = "Небьюла", LanguageId = 1, CountryId = 1, IsOpened = true },
                new Award { Id = 2, Name = "Hugo", RusName = "Хьюго", LanguageId = 1, CountryId = 2, IsOpened = true },
                new Award { Id = 3, Name = "Oscar", RusName = "Оскар", LanguageId = 2, CountryId = 1, IsOpened = false }
            };
            dbContext.Awards.AddRange(awards);
            await dbContext.SaveChangesAsync();

            //var contests = new List<Contest>
            //{
            //    new Contest { AwardId = 1, Name = "1992", Date = new DateTime(1992, 10, 15) },
            //    new Contest { AwardId = 1, Name = "1998", Date = new DateTime(1998, 10, 15) },
            //    new Contest { AwardId = 1,  Name = "2015", Date = new DateTime(2015, 10, 15) },
            //};
            //dbContext.Contests.AddRange(contests);
            //await dbContext.SaveChangesAsync();
        }
    }
}
