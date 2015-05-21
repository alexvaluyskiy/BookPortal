using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives.Models;

namespace Microsoft.AspNet.Mvc
{
    public class SingleObjectResult : ObjectResult
    {
        public SingleObjectResult(int statusCode, object value) : base(value)
        {
            StatusCode = statusCode;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var result = new SuccessResult<object> {Result = Value};
            Value = result;

            return base.ExecuteResultAsync(context);
        }
    }
}
