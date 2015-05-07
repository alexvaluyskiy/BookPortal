using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Infrastructure
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // TODO: print detailed model errors information
            if (context.ModelState.IsValid == false)
            {
                context.Result = new WrappedErrorResult(400, "Model is not valid");
            }
        }
    }
}
