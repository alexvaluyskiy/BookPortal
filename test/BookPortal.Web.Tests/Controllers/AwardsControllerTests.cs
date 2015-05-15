using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Controllers;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Moq;
using Moq.Language.Flow;
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

        [Fact]
        public async Task AwardsControllerAddAwardTest()
        {
            AwardResponse response = _fixture.Create<AwardResponse>();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.AddAwardAsync(It.IsAny<Award>())).ReturnsAsync(response);

            AwardsController controller = new AwardsController(awardsService.Object);

            Award request = new Award {Name = "Nebula"};

            IActionResult actionResult = await controller.Post(request);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);
            Assert.Equal(201, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerAddAwardFailedTest()
        {
            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.AddAwardAsync(It.IsAny<Award>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            Award request = new Award {Name = "Nebula"};

            IActionResult actionResult = await controller.Post(request);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);
            Assert.Equal(400, contentResult.StatusCode);
        }

        //[Fact]
        public async Task AwardsControllerUpdateAwardTest()
        {
            Award award = _fixture.Build<Award>().Without(c => c.Contests).Without(c => c.Nominations).Create();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.UpdateAwardAsync(It.IsAny<int>(), It.IsAny<Award>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            Award request = new Award { Name = "Nebula" };

            IActionResult actionResult = await controller.Put(award.Id, request);
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(204, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerUpdateAwardFailedTest()
        {
            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.UpdateAwardAsync(It.IsAny<int>(), It.IsAny<Award>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            Award request = new Award { Name = "Nebula" };

            IActionResult actionResult = await controller.Put(1, request);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);
            Assert.Equal(400, contentResult.StatusCode);
        }

        //[Fact]
        public async Task AwardsControllerDeleteAwardTest()
        {
            Award award = _fixture.Build<Award>().Without(c => c.Contests).Without(c => c.Nominations).Create();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.DeleteAwardAsync(It.IsAny<int>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Delete(award.Id);
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(204, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerDeleteAwardFailedTest()
        {
            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.DeleteAwardAsync(It.IsAny<int>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Delete(1);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);
            Assert.Equal(400, contentResult.StatusCode);
        }
    }
}
