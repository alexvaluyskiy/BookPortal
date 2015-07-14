using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Controllers;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Moq;
using Ploeh.AutoFixture;
using Xunit;

namespace BookPortal.Web.Tests.Controllers
{
    public class AwardsControllerTests
    {
        private readonly Fixture _fixture = new Fixture();

        //[Fact]
        public async Task AwardsControllerGetAwardsListTest()
        {
            var responses = _fixture.Create<List<AwardResponse>>();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.GetAwardsAsync(It.IsAny<AwardRequest>())).ReturnsAsync(responses);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Index(null);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);

            var content = contentResult.Value as IReadOnlyList<Award>;

            Assert.NotNull(content);
            Assert.Equal(3, content.Count);
            Assert.Equal(responses[0].Name, content[0].Name);
            //Assert.Equal(200, contentResult.StatusCode);
        }

        //[Fact]
        public async Task AwardsControllerGetAwardTest()
        {
            AwardResponse response = _fixture.Create<AwardResponse>();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.GetAwardAsync(It.IsAny<int>())).ReturnsAsync(response);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Get(response.Id);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);

            var content = contentResult.Value as Award;

            Assert.NotNull(content);
            Assert.Equal(response.Id, content.Id);
            Assert.Equal(response.Name, content.Name);
            //Assert.Equal(200, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerGetMissedAwardTest()
        {
            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.GetAwardAsync(It.IsAny<int>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Get(5);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);
            Assert.Equal(404, contentResult.StatusCode);
        }
    }
}
