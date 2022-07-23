using Backend.Application.DataTransferObjects.Shared;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Backend.Web.Extensions
{
    public static class JwtTokenConfig
    {
        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration, byte[] key)
        {
            var jwtSettingsSection = configuration.GetSection("JWT");
            services.Configure<JwtConfig>(jwtSettingsSection);
            var jwtConfig = jwtSettingsSection.Get<JwtConfig>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Fit" ,o=>
            {
               // var useKey = Encoding.UTF8.GetBytes(jwtConfig.Key);
                var useKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                //var useKey = Encoding.ASCII.GetBytes("BackendSecretKey1122#$%234");
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                   // IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    IssuerSigningKey = new SymmetricSecurityKey(useKey)
                };
            });

            
/*            services.AddAuthentication(*//*x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }*//*)

            // .AddJwtBearer("Fit", x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
                 //x.Events.OnMessageReceived = context => {

                 //    if (context.Request.Cookies.ContainsKey("authToken"))
                 //    {
                 //        context.Token = context.Request.Cookies["authToken"];
                 //    }

                 //    return Task.CompletedTask;
                 //};

             });*/
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Fit")
                    .Build();

                options.AddPolicy("FitPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes("Fit");
                    policy.RequireClaim("project_scope", "Fit");
                    //policy.RequireClaim(ClaimTypes.Role, _context.TblRoles.Where(x => x.ProjectId == 2).Select(x => x.Name).ToList());
                });
            });
            return services;
        }
    }
}
