using System;
using System.Linq;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    public class Startup
    {
        private IConfigurationRoot _Config;

        public void ConfigureServices(IServiceCollection services)
        {
            _Config = Config.Build();
            //_Config = new Config();

            services.AddHangfire(c => c.UseMemoryStorage());
            services.AddTransient<CheckLinkJob>();            
            services.AddTransient<LinkChecker>();
            services.Configure<OutputSettings>(_Config.GetSection("output"));
            services.Configure<SiteSettings>(_Config);
            var connectionString = @"Server=localhost;Database=Links;User id=sa;Password=Siya_2010";
            services.AddDbContext<LinksDb>(o => o.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app,IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            Logs.Init(loggerFactory, _Config);
            
            //Logs.Factory = loggerFactory;
            //Logs.Init(_Config);

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            //app.Run(async context => await context.Response.WriteAsync("We are doing good!"));            
        }
    }
}