using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using Okta.AspNetCore;
using Squip.Api.DomainModels;
using Squip.Api.Dtos;
using Squip.Api.Repositories;
using Squip.Api.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Squip.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Auth(services);
            Cors(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Squip API", Version = "v1" });
            });
            IoC(services);
        }

        private void IoC(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ISquipService, SquipService>();
            services.AddSingleton<ISquipRepository, SquipRepository>();
            services.AddTransient<IRepository<Idea>, IdeaRepository>();
            services.AddTransient<IRepository<Reaction>, ReactionRepository>();
            services.AddTransient<IRepository<Presentation>, PresentationRepository>();
            services.AddTransient<IUserService, OktaUserService>();
        }

        private void Cors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://app.squip.io").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });

                options.AddPolicy("local", builder =>
                {
                    builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
        }

        private void Auth(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
                })
                .AddOktaWebApi(new OktaWebApiOptions()
                {
                    OktaDomain = "https://dev-334709.okta.com"
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("local");
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseCors();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Squip API v1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
            AutoMapper();
        }

        private void AutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<IdeaDto, Idea>();
                cfg.CreateMap<Idea, IdeaDbModel>()
                    .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.ToArray()))
                    .ForMember(dest => dest.InstantCreatedAt, opt => opt.MapFrom(src => src.InstantCreatedAt.ToDateTimeUtc()));
                cfg.CreateMap<IdeaDbModel, Idea>()
                    .ForMember(dest => dest.InstantCreatedAt,
                        opt => opt.MapFrom(src =>
                            Instant.FromDateTimeUtc(DateTime.SpecifyKind(src.InstantCreatedAt, DateTimeKind.Utc))));
                cfg.CreateMap<Idea, Presentation>().ForMember(dest => dest.SquipId, opt => opt.MapFrom(src => src.Id));
                cfg.CreateMap<Presentation, PresentationDto>();
                cfg.CreateMap<ReactionDto, Reaction>();
            });
        }
    }
}
