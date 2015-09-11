using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using Xunit;

namespace BookPortal.Web.FunctionalTests
{
    public class AwardsApiTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;

        public AwardsApiTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task ShouldReturnsAwardsList()
        {
            var url = "/api/awards";

            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsJsonPageAsync<AwardResponse>();
            Assert.NotNull(content);
            Assert.Equal(25, content.Limit);
            Assert.Equal(0, content.Offset);
            Assert.Equal(324, content.TotalRows);
            Assert.Equal(25, content.Rows.Count);

            var actual = content.Rows.First();
            Assert.Equal(2, actual.AwardId);
        }

        [Fact]
        public async Task ShouldReturnsAwardsListWithProperLimitOffset()
        {
            var limit = 30;
            var offset = 10;
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
            var url = "/api/awards?sort=rusname";

            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsJsonPageAsync<AwardResponse>();
            Assert.NotNull(content);

            var actual = content.Rows.First();
            Assert.Equal(144, actual.AwardId);
        }

        [Fact]
        public async Task ShouldReturnsAwardsListWithSortByLanguage()
        {
            var url = "/api/awards?sort=language";

            var response = await _httpClientFixture.HttpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsJsonPageAsync<AwardResponse>();
            Assert.NotNull(content);

            var actual = content.Rows.First();
            Assert.Equal(132, actual.AwardId);
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
