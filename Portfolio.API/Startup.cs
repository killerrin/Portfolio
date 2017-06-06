using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models;
using Portfolio.API.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Portfolio.API.Middleware;

namespace Portfolio.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("secretappsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"secretappsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddLogging();
            services.AddCors();

            // Add Database - //services.AddDbContext<PortfolioContext>(opt => opt.UseInMemoryDatabase());
            var connection = Configuration["dbConnectionString"];
            services.AddDbContext<PortfolioContext>(options => options.UseSqlServer(connection));

            // Add Dependency Injection for our Repositories
            services.AddSingleton<IRepository<User>, UserRepository>();
            services.AddSingleton<IRepository<Models.Tag>, TagRepository>();
            services.AddSingleton<IRepository<TagType>, TagTypeRepository>();
            services.AddSingleton<IRepository<PortfolioItem>, PortfolioItemRepository>();
            services.AddSingleton<IRepository<PortfolioItemLink>, PortfolioItemLinkRepository>();
            services.AddSingleton<IRepository<RelatedItem>, RelatedItemRepository>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Portfolio API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Enable custom Middleware to force Cross Site Access
            app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder()
                .AddDefaultSecurePolicy()
                .AddCustomHeader("Access-Control-Allow-Origin", "http://localhost:3000")
                .AddCustomHeader("Access-Control-Allow-Methods", "OPTIONS, GET, POST, PUT, PATCH, DELETE")
                .AddCustomHeader("Access-Control-Allow-Headers", "X-PINGOTHER, Content-Type")
                .AddCustomHeader("X-Developer-Message", "<3 killerrin"));

            // Enable Logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(
                options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
            );

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API V1");
            });
        }
    }
}
