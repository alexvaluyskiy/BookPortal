using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using Ploeh.AutoFixture;
using Xunit;

namespace BookPortal.Core.Framework.Test
{
    public class SingleObjectResultTest
    {
        private readonly Fixture _fixture;

        public SingleObjectResultTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void SingleObjectResult_Object()
        {
            var expectedValue = "Text";
            var expectedStatus = 201;

            var singleObjectResult = new SingleObjectResult(expectedStatus, expectedValue);

            Assert.Equal(expectedStatus, singleObjectResult.StatusCode);
            Assert.Equal(expectedValue, singleObjectResult.Value);
        }

        [Fact]
        public void PageObjectResult_Object()
        {
            var expectedValue = "Text";
            var expectedStatus = 200;

            var singleObjectResult = new PageObjectResult(expectedStatus, expectedValue);

            Assert.Equal(expectedStatus, singleObjectResult.StatusCode);
            Assert.Equal(expectedValue, singleObjectResult.Value);
        }

        [Fact]
        public void PageObjectResult_RowsLimitOffset()
        {
            var expectedValue = "Text";
            var expectedStatus = 200;
            var totalRows = 244;
            var limit = 25;
            var offset = 10;

            HttpContext ctx = new DefaultHttpContext();
            RouteData routeData = new RouteData();
            ActionContext actx = new ActionContext(ctx, routeData, new ActionDescriptor());

            var pageObjectResult = new PageObjectResult(expectedStatus, expectedValue, totalRows, limit, offset);
            pageObjectResult.ExecuteResult(actx);

            Assert.Equal(expectedStatus, pageObjectResult.StatusCode);
            Assert.Equal(expectedValue, pageObjectResult.Value);
            Assert.Equal(totalRows, pageObjectResult.TotalRows);
            Assert.Equal(limit, pageObjectResult.Limit);
            Assert.Equal(offset, pageObjectResult.Offset);
        }
    }
}
