using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;

namespace Microsoft.AspNet.Mvc
{
    public class SingleObjectResult : ObjectResult
    {
        private readonly string _location;

        public SingleObjectResult(int statusCode, object value, string location) : this(statusCode, value)
        {
            _location = location;
        }

        public SingleObjectResult(int statusCode, object value) : base(value)
        {
            StatusCode = statusCode;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var result = new SuccessResult<object> { Result = Value };
            Value = result;

            if (!string.IsNullOrEmpty(_location))
            {
                context.HttpContext.Response.Headers.Set("Location", _location);
            }

            return base.ExecuteResultAsync(context);
        }
    }
}
