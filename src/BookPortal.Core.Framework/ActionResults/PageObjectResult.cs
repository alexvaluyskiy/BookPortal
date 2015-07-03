using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;

namespace Microsoft.AspNet.Mvc
{
    public class PageObjectResult : ObjectResult
    {
        public PageObjectResult(int statusCode, object value) : this(statusCode, value, 0, 0, 0)
        {
        }

        public PageObjectResult(int statusCode, object value, int totalrows, int limit, int offset) : base(value)
        {
            TotalRows = totalrows;
            Limit = limit;
            Offset = offset;
            StatusCode = statusCode;
        }

        public int TotalRows { get; }

        public int Limit { get; }

        public int Offset { get; }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var pageResult = new PageResult<object>();
            pageResult.TotalRows = TotalRows;
            pageResult.Limit = Limit;
            pageResult.Offset = Offset;
            pageResult.Rows = Value;

            var successResult = new SuccessResult<PageResult<object>> { Result = pageResult };
            Value = successResult;

            return base.ExecuteResultAsync(context);
        }
    }
}
