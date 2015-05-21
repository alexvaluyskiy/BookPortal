using System;
using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives.Models;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace Microsoft.AspNet.Mvc
{
    public class ErrorObjectResult : ObjectResult
    {
        private readonly ModelStateDictionary _modelStateDictionary;
        private readonly string _message = "There was an error processing your request.";

        public ErrorObjectResult() : base(true)
        {
        }

        public ErrorObjectResult(int httpStatusCode) : this(httpStatusCode, message: null)
        {
            StatusCode = httpStatusCode;
        }

        public ErrorObjectResult(int httpStatusCode, string message) : base(true)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            StatusCode = httpStatusCode;
            _message = message;
        }

        public ErrorObjectResult(int httpStatusCode, ModelStateDictionary modelStateDictionary) : base(true)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            _modelStateDictionary = modelStateDictionary;
            StatusCode = httpStatusCode;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            ErrorResult errorResult = new ErrorResult();
            errorResult.Message = _message;
            if (_modelStateDictionary != null)
            {
                errorResult.ModelErrors = new SerializableError(_modelStateDictionary);
            }

            Value = errorResult;
            
            return base.ExecuteResultAsync(context);
        }
    }
}
