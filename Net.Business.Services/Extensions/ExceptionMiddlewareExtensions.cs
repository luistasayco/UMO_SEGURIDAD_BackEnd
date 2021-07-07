using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Net.Business.Services
{
    public static class ExceptionMiddlewareExtensions
    {
        //, ILoggerManager logger
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                        await context.Response.WriteAsync(new DtoErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = contextFeature.Error.Message.ToString()
                        }.ToString());
                    }
                });
            });
        }
    }
}


public class DtoErrorDetails
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

}