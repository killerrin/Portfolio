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

namespace Portfolio.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddLogging();

            // Add Database
            //services.AddDbContext<PortfolioContext>(opt => opt.UseInMemoryDatabase());
            var connection = @"Data Source=ANDREWPC-SKYNET;Initial Catalog=portfolio;Integrated Security=True;Pooling=False";
            services.AddDbContext<PortfolioContext>(options => options.UseSqlServer(connection));

            // Add Dependency Injection for our Repositories
            services.AddSingleton<IRepository<ProgrammingLanguage>, ProgrammingLanguageRepository>();
            services.AddSingleton<IRepository<Framework>, FrameworkRepository>();
            services.AddSingleton<IRepository<Keyword>, KeywordRepository>();
            services.AddSingleton<IRepository<Category>, CategoryRepository>();
            services.AddSingleton<IRepository<PortfolioItem>, PortfolioItemRepository>();
            services.AddSingleton<IRepository<PortfolioItemLink>, PortfolioItemLinkRepository>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Portfolio API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseMvcWithDefaultRoute();
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
