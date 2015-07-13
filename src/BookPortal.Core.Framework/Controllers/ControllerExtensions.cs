using System.Collections.Generic;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace Microsoft.AspNet.Mvc
{
    public static class ControllerExtensions
    {
        public static SingleObjectResult SingleObject(this Controller controller, int statusCode, object content)
        {
            return new SingleObjectResult(statusCode, content);
        }

        public static SingleObjectResult SingleObject(this Controller controller, object content)
        {
            return new SingleObjectResult(200, content);
        }

        public static PageObjectResult PageObject<T>(this Controller controller, int statusCode, IEnumerable<T> content)
        {
            return new PageObjectResult(statusCode, content);
        }

        public static PageObjectResult PageObject(this Controller controller, object content)
        {
            return new PageObjectResult(200, content);
        }

        public static PageObjectResult PageObject(this Controller controller, object content, int totalrows)
        {
            return new PageObjectResult(200, content, totalrows, 0, 0);
        }

        public static PageObjectResult PageObject(this Controller controller, object content, int totalrows, int limit, int offset)
        {
            return new PageObjectResult(200, content, totalrows, limit, offset);
        }

        public static ErrorObjectResult ErrorObject(this Controller controller, int statusCode)
        {
            return new ErrorObjectResult(statusCode);
        }

        public static ErrorObjectResult ErrorObject(this Controller controller, int statusCode, string message)
        {
            return new ErrorObjectResult(statusCode, message);
        }

        public static ErrorObjectResult ErrorObject(this Controller controller, int statusCode, ModelStateDictionary modelState, bool includeErrorDetail)
        {
            return new ErrorObjectResult(statusCode, modelState);
        }
    }
}
