using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tictactoewebapi.Model;
using Microsoft.AspNetCore.Cors.Infrastructure;
using tictactoewebapi.Repositories;
using Microsoft.Extensions.Options;

namespace tictactoewebapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddRouting();
            //Add Cors support to the service
            services.AddCors();
            var policy = new CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;
            services.AddCors(configure => configure.AddPolicy("AllowAllClients", policy));

            services.AddOptions();
            services.Configure<ConfigurationOptions>(Configuration);
            services.Configure<ConfigurationOptions>(Configuration.GetSection("ConfigurationOptions"));

            services.AddMvc();
            //ConfigureDependencies(services);

        }

        void ConfigureDependencies(IServiceCollection services)
        {
         
            services.AddScoped<IUserRepository>(x => new UserRepository());
            services.AddScoped<IGameRepository>(x => new GameRepository());
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IGameRepository, GameRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
        
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("AllowAllClients");
            app.UseMvc();
            
        }
    }
}
