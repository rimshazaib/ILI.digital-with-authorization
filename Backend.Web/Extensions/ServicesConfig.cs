using Backend.Data.IRepositories.IJWTManager;
using Backend.Data.Repositories.JWTRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI.Repository;

namespace Backend.Web.Extensions
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
            services.AddScoped<Imovierepository, movierepository>();
            //services.AddScoped<IticketRepository, TicketRepository>();
            //services.AddScoped<TestService, TestService>();

            return services;
        }
    }
}
