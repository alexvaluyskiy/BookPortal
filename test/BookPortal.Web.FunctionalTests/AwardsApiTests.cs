using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using BookPortal.Web.Services.Interfaces;
using Microsoft.Framework.DependencyInjection;
using Moq;
using Moq.Language.Flow;
using Ploeh.AutoFixture;
using Xunit;

namespace BookPortal.Web.FunctionalTests
{
    public class AwardsApiTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;
        private readonly Fixture _fixture;

        public AwardsApiTests(HttpClientFixture httpClientFixture)
        {
            _fixture = new Fixture();
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task ShouldReturnsAwardsList()
        {
            var apiObject = _fixture.Create<ApiObject<AwardResponse>>();
            var awardService = new Mock<IAwardsService>();
            awardService.Setup(c => c.GetAwardsAsync(It.IsAny<AwardRequest>())).ReturnsAsync(apiObject);
            _httpClientFixture.ServiceCollection.AddInstance(awardService.Object);
            _httpClientFixture.CreateHttpClient();

            var url = "/api/awards";
            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsJsonPageAsync<AwardResponse>();
            Assert.NotNull(content);
            Assert.Equal(25, content.Limit);
            Assert.Equal(0, content.Offset);
            Assert.Equal(apiObject.TotalRows, content.TotalRows);
            Assert.Equal(apiObject.Values.Count, content.Rows.Count);

            var actual = content.Rows.First();
            var expected = apiObject.Values.First();
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.RusName, actual.RusName);
            Assert.Equal(expected.Homepage, actual.Homepage);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.DescriptionSource, actual.DescriptionSource);
            Assert.Equal(expected.Notes, actual.Notes);
            Assert.Equal(expected.AwardClosed, actual.AwardClosed);
            Assert.Equal(expected.IsOpened, actual.IsOpened);
            Assert.Equal(expected.LanguageId, actual.LanguageId);
            Assert.Equal(expected.LanguageName, actual.LanguageName);
            Assert.Equal(expected.CountryId, actual.CountryId);
            Assert.Equal(expected.CountryName, actual.CountryName);
            Assert.Equal(expected.FirstContestDate, actual.FirstContestDate);
            Assert.Equal(expected.LastContestDate, actual.LastContestDate);
        }

        [Fact]
        public async Task ShouldReturnsAwardsListWithProperLimitOffset()
        {
            var limit = 30;
            var offset = 10;
            
            var apiObject = _fixture.Create<ApiObject<AwardResponse>>();
            var awardService = new Mock<IAwardsService>();
            awardService.Setup(c => c.GetAwardsAsync(It.Is<AwardRequest>(x => x.Limit == limit && x.Offset == limit))).ReturnsAsync(apiObject);
            _httpClientFixture.ServiceCollection.AddInstance(awardService.Object);
            _httpClientFixture.CreateHttpClient();

            var url = $"/api/awards?limit={limit}&offset={offset}";
            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsJsonPageAsync<AwardResponse>();
            Assert.NotNull(content);
            Assert.Equal(limit, content.Limit);
            Assert.Equal(offset, content.Offset);
        }

        [Fact]
        public async Task ShouldReturnsAwardsListWithSortByName()
        {
            var sort = "rusname";

            var apiObject = _fixture.Create<ApiObject<AwardResponse>>();
            var awardService = new Mock<IAwardsService>();
            awardService.Setup(c => c.GetAwardsAsync(It.Is<AwardRequest>(x => x.Sort == AwardSort.Rusname))).ReturnsAsync(apiObject);
            _httpClientFixture.ServiceCollection.AddInstance(awardService.Object);
            _httpClientFixture.CreateHttpClient();

            var url = $"/api/awards?sort={sort}";

            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnsAwardsListWithSortByLanguage()
        {
            var sort = "language";

            var apiObject = _fixture.Create<ApiObject<AwardResponse>>();
            var awardService = new Mock<IAwardsService>();
            awardService.Setup(c => c.GetAwardsAsync(It.Is<AwardRequest>(x => x.Sort == AwardSort.Language))).ReturnsAsync(apiObject);
            _httpClientFixture.ServiceCollection.AddInstance(awardService.Object);
            _httpClientFixture.CreateHttpClient();

            var url = $"/api/awards?sort={sort}";

            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnsAwardsListWithSortByCountry()
        {
            var url = "/api/awards?sort=country";

            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsJsonPageAsync<AwardResponse>();
            Assert.NotNull(content);

            var actual = content.Rows.First();
            Assert.Equal(70, actual.AwardId);
        }

        [Fact]
        public async Task ShouldReturnsAwardsListWithOpenedOnly()
        {
            var url = "/api/awards?isopened=true";

            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsJsonPageAsync<AwardResponse>();
            Assert.NotNull(content);
            Assert.Equal(128, content.TotalRows);
            Assert.Equal(25, content.Rows.Count);
        }
    }
}
