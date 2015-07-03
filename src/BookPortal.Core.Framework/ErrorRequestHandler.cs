using System;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;

namespace BookPortal.Core.Framework
{
    public class ErrorRequestHandler
    {
        public static async Task HandleErrorRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var error = context.GetFeature<IErrorHandlerFeature>();
            if (error != null)
            {
                ErrorResult errorResult = new ErrorResult();
                errorResult.Message = error.Error.GetType().Name;
                errorResult.Details = error.Error.ToString();

                var errorText = JsonConvert.SerializeObject(errorResult);

                if (error.Error is ArgumentException || error.Error is ArgumentNullException)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(errorText);
                }
                else if (error.Error is NotImplementedException || error.Error is NotSupportedException)
                {
                    context.Response.StatusCode = 405;
                    await context.Response.WriteAsync(errorText);
                }
                else
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(errorText);
                }
            }
        }
    }
}
