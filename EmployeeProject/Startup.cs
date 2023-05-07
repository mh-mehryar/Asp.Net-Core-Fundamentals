using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public readonly IConfiguration configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //get single value from appsettings

            //var applicationUrl1 = configuration.GetValue(typeof(string), "ApplicationUrl");
            var applicationUrl = configuration.GetValue<string>("ApplicationUrl");

            //get object value from appsettings
            var host = configuration.GetSection("SiteInfo")["Host"];
            var port = configuration.GetSection("SiteInfo")["Port"];
            app.Run(async context =>
           await context.Response.WriteAsync("This Test is running in Default Environment ..."));


            //var environment = env.EnvironmentName;
            //if (env.IsDevelopment())
            //    //Custom Middleware
            //    app.UseCustomLogger();
            //else
            //    app.Run(async context =>
            //    await context.Response.WriteAsync(environment));

            #region Custom
            //MapWhen
            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), builder =>
            {
                builder.Run(async context =>
                {
                    var branch = context.Request.Query["branch"];
                    await context.Response.WriteAsync($"branch is {branch}");
                });
            });
            //UseWhen
            app.UseWhen(context => context.Request.Query.ContainsKey("title"), builder =>
            {
                builder.Run(async context =>
                {
                    var title = context.Request.Query["title"];
                    await context.Response.WriteAsync($"title is {title}");
                });
            });
            //Map
            //localhost:5001/products
            app.Map("/products", appbuilder =>
        {
            //localhost:5001/products/details
            appbuilder.Map("/details", HandleProductDetails());

            appbuilder.Use(async (context, next) =>
            {
                var name = context.Request.Query["name"];
                if (!string.IsNullOrWhiteSpace(name))
                    context.Items.Add("name", name);
                await next.Invoke();
            });

            appbuilder.Run(async context =>
            {
                var name = context.Items["name"];
                if (name == null)
                    await context.Response.WriteAsync("default name is : Mohammad Hossein");
                else
                    await context.Response.WriteAsync($"my name is : {name}");
            });
        });


            //Use
            app.Use(async (context, next) =>
            {
                context.Items.Add("name", "Hossein");
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                var id = context.Request.Query["id"];
                await context.Response.WriteAsync("This Response Created by Use Method ...");
            });



            //Run
            app.Run(async context =>
            {

                //context.Items.TryGetValue("name", out var name);
                var name = context.Items["name"];
                await context.Response.WriteAsync("Run Executed Successfully ...");
            });

            app.Run(async context =>
            await context.Response.WriteAsync("Second Executed Successfully ..."));

            #endregion
            #region Default Middlewares
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            #endregion
        }

        private static Action<IApplicationBuilder> HandleProductDetails()
        {
            return builder =>
            {
                builder.Run(async context =>
                {
                    await context.Response.WriteAsync("Welcome to Details page ...");
                });
            };
        }

        //public void ConfigureProduction(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //   await context.Response.WriteAsync("This Test is in Production Environment ..."));
        //}

        //public void ConfigureDevelopment(IApplicationBuilder app)
        //{
        //    app.UseCustomLogger();
            
        //}
        //public void ConfigureStaging(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //    await context.Response.WriteAsync("This Test is in Staging Environment ..."));

        //}
    }
}
