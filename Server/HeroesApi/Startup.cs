using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime.Configuration;

namespace HeroesApi
{
    public class Startup
    {
	    private IClusterClient _client;

	    public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

	        var config = ClientConfiguration.LocalhostSilo();
			config.OpenConnectionTimeout = TimeSpan.FromMinutes(2);

	        _client = new ClientBuilder().UseConfiguration(config).Build();

	        _client.Connect().Wait();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
	        services.AddSingleton(provider => _client);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

	        if (env.IsDevelopment())
	        {
		        app.UseDeveloperExceptionPage();
	        }

            app.UseMvc();
        }
    }
}
