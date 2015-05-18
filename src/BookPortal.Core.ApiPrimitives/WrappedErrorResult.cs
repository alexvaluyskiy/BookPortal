using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace BookPortal.Core.ApiPrimitives
{
    // TODO: accept list of errors
    public class WrappedErrorResult : ObjectResult
    {
        private readonly IEnumerable<ModelError> _modelErrors;
        private readonly string _errorMessage;

        public WrappedErrorResult() : base(true)
        {
        }

        public WrappedErrorResult(int httpStatusCode) : this(httpStatusCode, errorMessage: null)
        {
            StatusCode = httpStatusCode;
        }

        public WrappedErrorResult(int httpStatusCode, string errorMessage) : base(true)
        {
            StatusCode = httpStatusCode;
            _errorMessage = errorMessage;
        }

        public WrappedErrorResult(int httpStatusCode, IEnumerable<ModelError> modelErrors) : base(true)
        {
            _modelErrors = modelErrors;
            StatusCode = httpStatusCode;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            ApiError apiError = new ApiError();

            if (_modelErrors != null)
            {
                apiError.Message = "The model submitted was invalid. Please correct the specified errors and try again.";
                apiError.ModelErrors = _modelErrors;
            }
            else if (!string.IsNullOrEmpty(_errorMessage))
            {
                apiError.Message = _errorMessage;
            }
            else
            {
                apiError.Message = "Unexpected error";
            }

            Value = apiError;
            return base.ExecuteResultAsync(context);
        }
    }
}
