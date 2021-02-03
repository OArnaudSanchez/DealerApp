using System;
using System.IO;
using System.Text;
using AutoMapper;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Interfaces;
using DealerApp.Core.Services;
using DealerApp.Core.Validations;
using DealerApp.Infrastructure.Data;
using DealerApp.Infrastructure.Filters;
using DealerApp.Infrastructure.Options;
using DealerApp.Infrastructure.Repositories;
using DealerApp.Infrastructure.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DealerApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DealerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AzureSQLConnection"));
            });
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IPagedGenerator<>), typeof(PagedGenerator<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IEmailValidation, EmailValidation>();
            services.AddTransient<IDniValidation, DniValidation>();
            services.AddTransient<IPhoneNumberValidation, PhoneValidation>();
            services.AddTransient<IFechaValidation, FechaValidation>();
            services.AddTransient<ISangreValidation, SangreValidation>();
            services.AddTransient<IRolValidation, RolValidation>();
            services.AddTransient<IHelperImage, ImageService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<ICombustibleService, CombustibleService>();
            services.AddTransient<IContratoService, ContratoService>();
            services.AddTransient<IMarcaService, MarcaService>();
            services.AddTransient<IModeloService, ModeloService>();
            services.AddTransient<IRolService, RolService>();
            services.AddTransient<ISangreClienteService, SangreClienteService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IVehiculoService, VehiculoService>();
            services.AddTransient<IPasswordHasher, PasswordService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ILoginRepository, LoginRepository>();


            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });

            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Dealer API", Version = "v1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = Configuration["Authentication:Issuer"],
                  ValidAudience = Configuration["Authentication:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
              };
          });
            return services;
        }

        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(options =>
           {
               options.Filters.Add<ValidationFilter>();
           })
           .AddFluentValidation(options =>
           {
               options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
           });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        public static IServiceCollection AddControllersConfigurations(this IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>())
           .AddNewtonsoftJson(options =>
           {
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
           });
            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dealer API V1");
                options.RoutePrefix = string.Empty;
            });
            return app;
        }

        public static IApplicationBuilder UseStaticFilesConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                                Path.Combine(env.ContentRootPath,"wwwroot","Resources","Images")),
                RequestPath = "/wwwroot/Resources"
            });
            return app;
        }
    }
}