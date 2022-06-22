using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
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
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Auth0:Authority"];
                    options.Audience = Configuration["Auth0:Audience"];
                });
            services.AddAuthorization();

            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddControllers();
            services.AddDbContext<SquipContext>(
                options =>
                    options.UseNpgsql(
                        Configuration.GetConnectionString("SquipDatabase"),
                        x => x.UseNodaTime()
                    )
            );
            if (_env.IsDevelopment())
            {
                services.AddCors(
                    options =>
                        options.AddDefaultPolicy(
                            policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                        )
                );
                services.AddScoped<IRepository<Idea>, EfIdeaRepository>();
                services.AddScoped<ISquipRepository, EfIdeaRepository>();
                services.AddSwaggerGen();
            }
            else
            {
                services.AddCors(
                    options =>
                        options.AddDefaultPolicy(
                            policy =>
                                policy
                                    .WithOrigins("https://squip-project.web.app")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                        )
                );
            }

            services.AddScoped<IRepository<Idea>, EfIdeaRepository>();
            services.AddScoped<ISquipRepository, EfIdeaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
