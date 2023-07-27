using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;
using Squip.Rest.Ideas.Domain;
using Squip.Rest.Infrastructure.Common;
using Squip.Rest.Infrastructure.EntityFramework;
using Squip.Rest.Users.Services;

namespace Squip.Rest;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _env = env;
        _configuration = configuration;
    }

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
                options.Authority = _configuration["Auth0:Authority"];
                options.Audience = _configuration["Auth0:Audience"];

                // This makes the UserId present in the User.Identity.Name property
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
        services.AddAuthorization();

        if (!_env.IsDevelopment())
            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            });
        ;
        services.AddDbContext<SquipContext>(
            options =>
                options.UseNpgsql(
                    _configuration.GetConnectionString("SquipDatabase"),
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
            services.AddScoped<IUserIdProvider, UserIdProvider>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Squip", Version = "v1" });
                c.AddSecurityDefinition(
                    "token",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer",
                        In = ParameterLocation.Header,
                        Name = "Authorization"
                    }
                );
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "token"
                                },
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            });
        }
        else
        {
            services.AddCors(
                options =>
                    options.AddDefaultPolicy(
                        policy =>
                            policy
                                .WithOrigins(
                                    "https://squip-project.web.app",
                                    "https://app.squip.ai"
                                )
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                    )
            );
            services.AddScoped<IUserIdProvider, UserIdProvider>();
        }

        services.AddScoped<IRepository<Idea>, EfIdeaRepository>();
        services.AddScoped<ISquipRepository, EfIdeaRepository>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

        app.UseSerilogRequestLogging();
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
