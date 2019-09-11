using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var exceptionMessage = JsonConvert.SerializeObject(new { error = exception.Message });
            if (exception is CultureNotFoundException) code = HttpStatusCode.InternalServerError;
            //else if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is ForbiddenException) code = HttpStatusCode.Forbidden;
            //else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;
            else if(exception is DbUpdateConcurrencyException)
            {
                code = HttpStatusCode.Conflict;
                //exceptionMessage = JsonConvert.SerializeObject(new
                //{
                //    error = Localizer["StartItemNotExist"]
                //});
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(exceptionMessage);
        }
    }
}
