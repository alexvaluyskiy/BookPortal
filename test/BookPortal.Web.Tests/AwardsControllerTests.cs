using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace BookPortal.Web.Tests
{
    public class AwardsControllerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task AwardsControllerGetAwardsListTest()
        {
            var awards = new List<Award>();

            foreach (var i in Enumerable.Range(1, 3))
            {
                Award award = _fixture.Build<Award>().Without(c => c.Contests).Without(c => c.Nominations).Create();
                awards.Add(award);
            }

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.GetAwardsAsync()).ReturnsAsync(awards);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Get();
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);

            var content = contentResult.Value as IList<Award>;

            Assert.NotNull(content);
            Assert.Equal(3, content.Count);
            Assert.Equal(awards[0].Name, content[0].Name);
            //Assert.Equal(200, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerGetAwardTest()
        {
            Award award = _fixture.Build<Award>().Without(c => c.Contests).Without(c => c.Nominations).Create();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.GetAwardAsync(It.IsAny<int>())).ReturnsAsync(award);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Get(award.Id);
            var contentResult = actionResult as ObjectResult;

            Assert.NotNull(contentResult);

            var content = contentResult.Value as Award;

            Assert.NotNull(content);
            Assert.Equal(award.Id, content.Id);
            Assert.Equal(award.Name, content.Name);
            //Assert.Equal(200, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerGetMissedAwardTest()
        {
            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.GetAwardAsync(It.IsAny<int>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            IActionResult actionResult = await controller.Get(5);
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(404, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerAddAwardTest()
        {
            Award award = _fixture.Build<Award>().Without(c => c.Contests).Without(c => c.Nominations).Create();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.AddAwardAsync(It.IsAny<AwardRequest>())).ReturnsAsync(award);

            AwardsController controller = new AwardsController(awardsService.Object);

            AwardRequest request = new AwardRequest {Name = "Nebula"};

            IActionResult actionResult = await controller.Post(request);
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(201, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerAddAwardFailedTest()
        {
            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.AddAwardAsync(It.IsAny<AwardRequest>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            AwardRequest request = new AwardRequest {Name = "Nebula"};

            IActionResult actionResult = await controller.Post(request);
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(400, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerUpdateAwardTest()
        {
            Award award = _fixture.Build<Award>().Without(c => c.Contests).Without(c => c.Nominations).Create();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.UpdateAwardAsync(It.IsAny<int>(), It.IsAny<AwardRequest>())).ReturnsAsync(award);

            AwardsController controller = new AwardsController(awardsService.Object);

            AwardRequest request = new AwardRequest { Name = "Nebula" };

            IActionResult actionResult = await controller.Put(award.Id, request);
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(204, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerUpdateAwardFailedTest()
        {
            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.UpdateAwardAsync(It.IsAny<int>(), It.IsAny<AwardRequest>())).ReturnsAsync(null);

            AwardsController controller = new AwardsController(awardsService.Object);

            AwardRequest request = new AwardRequest { Name = "Nebula" };

            IActionResult actionResult = await controller.Put(1, request);
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(400, contentResult.StatusCode);
        }

        [Fact]
        public async Task AwardsControllerDeleteAwardTest()
        {
            Award award = _fixture.Build<Award>().Without(c => c.Contests).Without(c => c.Nominations).Create();

            var awardsService = new Mock<AwardsService>(null);
            awardsService.Setup(c => c.DeleteAwardAsync(It.IsAny<int>())).ReturnsAsync(award);

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
            var contentResult = actionResult as HttpStatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(400, contentResult.StatusCode);
        }
    }
}
