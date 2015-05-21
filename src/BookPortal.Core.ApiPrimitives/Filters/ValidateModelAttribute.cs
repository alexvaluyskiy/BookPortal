using Microsoft.AspNet.Mvc;

namespace BookPortal.Core.ApiPrimitives.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new ErrorObjectResult(400, context.ModelState);
            }
        }
    }
}
