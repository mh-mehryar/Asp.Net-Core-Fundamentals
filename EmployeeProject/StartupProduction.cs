using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeProject
{
    public class StartupProduction
    {
        public void ConfigureServices(IServiceCollection services)
        {

        }
        public void Configure(IApplicationBuilder app)
        {
           app.Run(async context =>
           await context.Response.WriteAsync("We are running StartupProduction Class now ..."));
        }
    }
}
