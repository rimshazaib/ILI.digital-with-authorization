//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Backend.Application;
using Backend.Application.DataTransferObjects.Shared;
//using Microsoft.IdentityModel.Tokens;
//using Quartz;
using Backend.Data;
using Backend.Web.Extensions;
using Backend.Web.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Backend.Web
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplicationLayer();
            //services.AddScoped<IMoviesService, MovieServices>();
            //services.AddRazorPages();
            
            //services.AddApplicationLayer();
            DataBaseSettings.ConnectionString = Configuration["DataBaseSettings:ConnectionString"];
            services.AddControllers();
            //AWSS3Model.AccessKey = Configuration["AWSS3:AccessKey"];
            //AWSS3Model.SecretKey = Configuration["AWSS3:SecretKey"];
            //services.AddApplicationLayer();
            //NEW
            /*   services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<EFDataContext>()
           .AddDefaultTokenProviders();*/
            //NEW
            /*  services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieAPI1", Version = "v1" });
              });*/
            services.AddCors();
            // For Entity Framework  

            //NEW
         services.AddDbContext<EFDataContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            //var key = System.Text.Encoding.UTF8.GetBytes(Logging.JWT.Key);
            //var key = System.Text.Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);


            //services.AddControllersWithViews(options =>
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            //services.AddAntiforgery(options =>
            //{
            //    //options.FormFieldName = "MyAntiForgeryField";
            //    //options.HeaderName = "MyAntiForgeryHeader";
            //    options.Cookie.Name = "authToken";
            //});
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
            //    o.SaveToken = true;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["JWT:Issuer"],
            //        ValidAudience = Configuration["JWT:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Key)
            //    };
            //});
            services.AddJwtTokenAuthentication(Configuration,key);
            services.AddServicesConfig(Configuration);
            services.AddAdminServicesConfig(Configuration);
            services.AddControllers(options => options.EnableEndpointRouting = false)
                    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddApiVersioningExtension();
            services.AddSwaggerConfiguration();
            services.AddHealthChecks();
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            // If using Kestrel:  
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:  
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // migrate any database changes on startup (includes initial db creation)
            //dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.Web v1"));
            }

            //app.UseHttpsRedirection();
            app.UseSwaggerSetup();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 //  .AllowCredentials() // allow credentials
                 );

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
       /*         endpoints.MapRazorPages();*/
            });


        }
    }
}

