using System.Linq;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Core.ApiPrimitives
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var modelErrors = context.ModelState.SelectMany(m => m.Value.Errors);
                context.Result = new WrappedErrorResult(400, modelErrors);
            }
        }
    }
}
