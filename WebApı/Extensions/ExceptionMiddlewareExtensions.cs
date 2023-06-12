using Entitites.ErrorModels;
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
                        logger.LogError($"Something went wrong:{contextFuture.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error"
                        }.ToString());
                    }
                });
            });
        }
    }
}
