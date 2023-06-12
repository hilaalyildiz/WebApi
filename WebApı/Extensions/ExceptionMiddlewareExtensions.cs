using Entitites.ErrorModels;
using Entitites.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Net;

namespace WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app,ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFuture = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFuture is not null)
                    {
                        context.Response.StatusCode = contextFuture.Error switch
                        {
                            NotFoundExceptions => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong:{contextFuture.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFuture.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
