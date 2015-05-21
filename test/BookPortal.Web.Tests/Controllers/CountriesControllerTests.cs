using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Controllers;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Moq;
using Moq.Language.Flow;
using Ploeh.AutoFixture;
using Xunit;

namespace BookPortal.Web.Tests.Controllers
{
    public class CountriesControllerTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task GetCountriesTest()
        {
            var countries = _fixture.CreateMany<Country>().ToList();
            var countriesService = new Mock<CountriesService>(null);
            countriesService.Setup(c => c.GetCountriesAsync()).ReturnsAsync(countries);

            var controller = new CountriesController(countriesService.Object);
            var actionResult = (ObjectResult)(await controller.Index());
            var content = actionResult?.Value as List<Country>;

            Assert.NotNull(actionResult);
            Assert.NotNull(content);
            Assert.Equal(3, content.Count);
            Assert.Equal(countries[0].Name, content[0].Name);
            //Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetCountriesIdTest()
        {
            var country = _fixture.Create<Country>();
            var countriesService = new Mock<CountriesService>(null);
            countriesService.Setup(c => c.GetCountryAsync(It.IsAny<int>())).ReturnsAsync(country);

            var controller = new CountriesController(countriesService.Object);
            var actionResult = (ObjectResult)(await controller.Get(It.IsAny<int>()));
            var content = actionResult?.Value as Country;

            Assert.NotNull(actionResult);
            Assert.NotNull(content);
            Assert.Equal(country.Id, content.Id);
            Assert.Equal(country.Name, content.Name);
            //Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetCountriesIdNotFoundTest()
        {
            var countriesService = new Mock<CountriesService>(null);
            countriesService.Setup(c => c.GetCountryAsync(It.IsAny<int>())).ReturnsAsync(null);

            var controller = new CountriesController(countriesService.Object);
            var actionResult = (ObjectResult)(await controller.Get(It.IsAny<int>()));

            Assert.NotNull(actionResult);
            Assert.Equal(404, actionResult.StatusCode);
        }
    }
}
