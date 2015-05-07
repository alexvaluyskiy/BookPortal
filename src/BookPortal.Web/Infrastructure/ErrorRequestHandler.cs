using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Api;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;

namespace BookPortal.Web.Infrastructure
{
    public class ErrorRequestHandler
    {
        public static async Task HandleErrorRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var error = context.GetFeature<IErrorHandlerFeature>();
            if (error != null)
            {
                AggregateException aggregateException = error.Error as AggregateException;

                if (aggregateException != null)
                {
                    ApiError apiError = new ApiError();
                    apiError.Message = aggregateException.Message;

                    foreach (var exception in aggregateException.InnerExceptions)
                    {
                        ApiErrorItem apiErrorItem = new ApiErrorItem();
                        apiErrorItem.Message = exception.Message;
                        apiErrorItem.Details = exception.StackTrace;
                        apiErrorItem.ErrorCode = exception.GetType().Name;
                        apiError.Errors = new List<ApiErrorItem>();
                        apiError.Errors.Add(apiErrorItem);
                    }

                    var errorText = JsonConvert.SerializeObject(apiError);

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(errorText);
                }
                else
                {
                    Exception exception = error.Error;

                    ApiErrorItem apiErrorItem = new ApiErrorItem();
                    apiErrorItem.Message = exception.Message;
                    apiErrorItem.Details = exception.StackTrace;
                    apiErrorItem.ErrorCode = exception.GetType().Name;

                    var errorText = JsonConvert.SerializeObject(apiErrorItem);

                    if (exception is ArgumentException || exception is ArgumentNullException)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(errorText);
                    }
                    else if (exception is NotImplementedException || exception is NotSupportedException)
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
}
