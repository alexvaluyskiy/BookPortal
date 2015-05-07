using System.Threading.Tasks;
using BookPortal.Web.Models.Api;
using Microsoft.AspNet.Mvc;

namespace Microsoft.AspNet.Mvc
{
    public class WrappedErrorResult : ObjectResult
    {
        private readonly string _errorMessage;

        public WrappedErrorResult() : base(true)
        {
        }

        public WrappedErrorResult(int httpStatusCode) : this(httpStatusCode, null)
        {
            StatusCode = httpStatusCode;
        }

        public WrappedErrorResult(int httpStatusCode, string errorMessage) : base(true)
        {
            StatusCode = httpStatusCode;
            _errorMessage = errorMessage;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            ApiErrorItem apiErrorItem = new ApiErrorItem();
            apiErrorItem.Message = !string.IsNullOrEmpty(_errorMessage) ? _errorMessage : "unexpected error";

            Value = apiErrorItem;
            return base.ExecuteResultAsync(context);
        }
    }
}
