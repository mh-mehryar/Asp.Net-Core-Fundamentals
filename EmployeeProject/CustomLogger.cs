using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EmployeeProject
{
    public class CustomLogger
    {
        private RequestDelegate _next;

        public CustomLogger(RequestDelegate next)
        {
            _next = next;
        }
        //Body of Middleware
        public Task Invoke(HttpContext context)
        {
            //some logic
            var title = context.Request.Query["title"];
            Console.WriteLine(title);
         
            return _next(context);

        }

    }

    public static class CustomLoggerExtensions
    {
        public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<CustomLogger>();
        }
    }
}
