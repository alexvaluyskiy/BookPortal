using System.Collections.Generic;
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
    public class ContestsServiceTest
    {
        private readonly ContestsService _service;

        public ContestsServiceTest()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<BookContext>(c => c.UseInMemoryDatabase());

            var serviceProvider = services.BuildServiceProvider();

            var dbContext = serviceProvider.GetRequiredService<BookContext>();
            CreateSampleData(dbContext);

            _service = new ContestsService(dbContext);
        }

        [Fact]
        public async Task GetContests_Success()
        {
            int awardId = 1;
            var response = await _service.GetContestsAsync(awardId);

            Assert.NotNull(response);
            Assert.Equal(2, response.Count);

            var actual = response[0];
            Assert.IsType<ContestResponse>(actual);
            Assert.Equal(5, actual.ContestId);
            Assert.Equal("2006", actual.Name);
            Assert.Equal(2006, actual.NameYear);
            Assert.Equal("USA", actual.Place);
            Assert.Equal("2006-10-12", actual.Date);
            Assert.Equal("text", actual.Description);
            Assert.Equal(awardId, actual.AwardId);
        }

        [Fact]
        public async Task GetOneContest_Success()
        {
            int awardId = 1;
            int contestId = 5;
            var actual = await _service.GetContestAsync(awardId, contestId);

            Assert.NotNull(actual);
            Assert.IsType<ContestResponse>(actual);
            Assert.Equal(contestId, actual.ContestId);
            Assert.Equal("2006", actual.Name);
            Assert.Equal(2006, actual.NameYear);
            Assert.Equal("USA", actual.Place);
            Assert.Equal("2006-10-12", actual.Date);
            Assert.Equal("text", actual.Description);
            Assert.Equal(awardId, actual.AwardId);
        }

        [Fact]
        public async Task GetOneContest_WithoutAward()
        {
            int awardId = 2;
            int contestId = 5;
            var actual = await _service.GetContestAsync(awardId, contestId);

            Assert.Null(actual);
        }

        private void CreateSampleData(BookContext dbContext)
        {
            var contests = new List<Contest>
            {
                new Contest { Id = 4, Name = "2005", NameYear = 2005, Place = "USA", Date = "2005-10-12", Description = "text", AwardId = 1 },
                new Contest { Id = 5, Name = "2006", NameYear = 2006, Place = "USA", Date = "2006-10-12", Description = "text", AwardId = 1 },
            };

            dbContext.Contests.AddRange(contests);
            dbContext.SaveChanges();
        }
    }
}
