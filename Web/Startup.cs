using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public IConfigurationRoot Configuration { get; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // Setup our static files since not everything is in wwwroot.
            // allow for .less to be fetched, somehow, Angular 2 broke less.js and I am no longer 
            // using less pre-transpiled in the browser.
            // with another mention - I only used a single css file, where my intent was to specify the 
            // css dependencies in the component.
            app.UseStaticFiles();
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".less"] = "text/css";
            app.UseStaticFiles( new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css"))
                , RequestPath = new PathString("/css")
                , ContentTypeProvider = provider
            });
            // node (angular) files.  I was saddened to learn I had to download the entire angular dependencies due to TypeScript
            provider = new FileExtensionContentTypeProvider();
            app.UseStaticFiles( new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules"))
                , RequestPath = new PathString("/node_modules")
                , ContentTypeProvider = provider
            });
            // conventionally in CLI this folder is called src.  But i did not find that fitting for the MVC environment.
            // as I have "src" in more locations than just the Angular directory.
            app.UseStaticFiles( new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), "Angular"))
                , RequestPath = new PathString("")
                , ContentTypeProvider = provider
            });
            // These wwwroot folders may be redundant but i was unsure the impact messign with the UseStatiFiles is.
            // TODO investigate how wwwroot becomes a static folder
            app.UseStaticFiles( new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images"))
                , RequestPath = new PathString("/images")
                , ContentTypeProvider = provider
            });
            // setup our home controller route and the SPA dropthrough.
            // I understand the fall though is required for SPA applications where angular is handlign the routing.
            // unfortunatly I never had time to setup routing in the Angular application.
            // had i had the time, I woudl have set a route for each individual roadway camera allowing browser bookmarking.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
