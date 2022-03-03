using Biosero.Data.Repositories;
using Biosero.Service.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Biosero.Api.Middleware
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<BookService>();
            services.AddScoped<BookRepository>();

            services.AddScoped<AuthenticationService>();
            services.AddScoped<UserRepository>();

            return services;
        }
    }
}
