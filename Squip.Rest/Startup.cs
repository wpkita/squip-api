using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Squip.Rest.Domain;
using Squip.Rest.Repositories;

namespace Squip.Rest
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            if (_env.IsDevelopment())
            {
                services.AddCors(
                    options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin())
                );
                services.AddSingleton<IRepository<Tile>, InMemoryRepository<Tile>>();
                services.AddSingleton<IRepository<Habit>, InMemoryRepository<Habit>>();
                services.AddSingleton<IRepository<Idea>, InMemoryRepository<Idea>>();
                services.AddSingleton<ISquipRepository, InMemorySquipRepository>();
            }
            else
            {
                services.AddScoped<IRepository<Tile>, TileCosmosRepository>();
                services.AddApplicationInsightsTelemetry(
                    Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]
                );
            }
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // "The call to UseCors must be placed after UseRouting, but before UseAuthorization."
            if (env.IsDevelopment())
            {
                app.UseCors();
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
