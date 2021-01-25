using System;
using AutoMapper;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Interfaces;
using DealerApp.Core.Services;
using DealerApp.Infrastructure.Data;
using DealerApp.Infrastructure.Filters;
using DealerApp.Infrastructure.Repositories;
using DealerApp.Infrastructure.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using DealerApp.Core.Validations;
using Microsoft.Extensions.FileProviders;
using System.IO;
using DealerApp.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DealerApp.API
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
            services.AddDbContext<DealerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DealerApp"));
            });
            
            services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>())
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });


            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			
			app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Resources//Images")),
                RequestPath = "/Resources"
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
