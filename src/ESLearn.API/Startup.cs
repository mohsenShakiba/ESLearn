using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESLearn.Repository.ElasticSearch;
using ESLearn.Repository.Mappings;
using ESLearn.Repository.Repositories;
using ESLearn.API.Extensions;
using ESLearn.Domain.AggregatesModel.PostsAggregate;
using ESLearn.Domain.AggregatesModel.UsersAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using AppContext = System.AppContext;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ESLearn.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(hostingEnvironment.ContentRootPath)
//                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
//                .AddEnvironmentVariables();

            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            var elasticUri = Configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();
            
            // adding elastic client as singleton
            // this setting will be default use the single node connection pool
            services.ConfigureElasticSearchContext<ESLearn.Repository.ElasticSearch.AppContext>(elasticUri);

            // adding repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPostsRepository, PostsRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}