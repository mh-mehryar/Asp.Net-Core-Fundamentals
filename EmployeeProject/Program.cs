using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace EmployeeProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public static IHostBuilder CreateHostBuilder(string[] args)
        //{
        //    var assembly = typeof(Startup).GetTypeInfo().Assembly.FullName;
        //    return Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             //webBuilder.UseStartup<Startup>();
        //             webBuilder.UseStartup(assembly);
        //         });     
        //}
    }
}
