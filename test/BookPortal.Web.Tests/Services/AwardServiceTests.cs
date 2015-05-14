using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Infrastructure;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
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
                      .AddInMemoryStore()
                      .AddDbContext<BookContext>();

            _serviceProvider = services.BuildServiceProvider();

            MapperInitialization.Initialize();
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
            Assert.Equal(3, response.Count);
            Assert.Equal(1, response[0].Id);
            Assert.Equal("Nebula", response[0].Name);

            Assert.NotNull(response[0].FirstContestDate);
            Assert.Equal(1992, response[0].FirstContestDate.Value.Year);
            Assert.NotNull(response[0].LastContestDate);
            Assert.Equal(2015, response[0].LastContestDate.Value.Year);
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
            Assert.Equal(2, response.Count);
            Assert.Equal(1, response[0].Id);
            Assert.Equal("Nebula", response[0].Name);
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
            Assert.Equal(1, response.Count);
            Assert.Equal(2, response[0].Id);
            Assert.Equal("Hugo", response[0].Name);
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
            Assert.Equal(3, response.Count);
            Assert.Equal("Nebula", response[0].Name);
            Assert.Equal("Oscar", response[1].Name);
            Assert.Equal("Hugo", response[2].Name);
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
            Assert.Equal(3, response.Count);
            Assert.Equal("Oscar", response[0].Name);
            Assert.Equal("Nebula", response[1].Name);
            Assert.Equal("Hugo", response[2].Name);
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
            Assert.Equal(3, response.Count);
            Assert.Equal("Hugo", response[0].Name);
            Assert.Equal("Nebula", response[1].Name);
            Assert.Equal("Oscar", response[2].Name);
        }

        [Fact]
        public async Task GetAwardTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            await CreateSampleTask(dbContext);

            var service = new AwardsService(dbContext);

            var award = await service.GetAwardAsync(1);

            Assert.NotNull(award);
            Assert.Equal(1, award.Id);
            Assert.Equal("Nebula", award.Name);
            Assert.NotNull(award.FirstContestDate);
            Assert.Equal(1992, award.FirstContestDate.Value.Year);
            Assert.NotNull(award.LastContestDate);
            Assert.Equal(2015, award.LastContestDate.Value.Year);
        }

        [Fact]
        public async Task AddNewAwardTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            var service = new AwardsService(dbContext);

            var award = new Award { Id = 1, Name = "Hugo" };

            var response = await service.AddAwardAsync(award);

            Assert.NotNull(response);
            Assert.Equal(response.Name, response.Name);
            Assert.Equal(award.Id, response.Id);
        }

        [Fact]
        public async Task UpdateAwardTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            var service = new AwardsService(dbContext);

            var award = new Award { Id = 1, Name = "Hugo" };
            dbContext.Awards.Add(award);
            await dbContext.SaveChangesAsync();

            award.RusName = "Хьюго";
            var response = await service.UpdateAwardAsync(award.Id, award);

            Assert.NotNull(response);
            Assert.Equal(response.Name, response.Name);
            Assert.Equal(award.RusName, response.RusName);
        }

        [Fact]
        public async Task DeleteAwardTest()
        {
            var dbContext = _serviceProvider.GetRequiredService<BookContext>();
            var service = new AwardsService(dbContext);

            var award = new Award { Id = 1, Name = "Hugo" };
            dbContext.Awards.Add(award);
            await dbContext.SaveChangesAsync();

            var countBefore = dbContext.Awards.Count();
            var response = await service.DeleteAwardAsync(award.Id);
            var countAfter = dbContext.Awards.Count();

            Assert.NotNull(response);
            Assert.Equal(1, countBefore);
            Assert.Equal(0, countAfter);
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

            var contests = new List<Contest>
            {
                new Contest { AwardId = 1, Name = "1992", Date = new DateTime(1992, 10, 15) },
                new Contest { AwardId = 1, Name = "1998", Date = new DateTime(1998, 10, 15) },
                new Contest { AwardId = 1,  Name = "2015", Date = new DateTime(2015, 10, 15) },
            };
            dbContext.Contests.AddRange(contests);
            await dbContext.SaveChangesAsync();
        }
    }
}
