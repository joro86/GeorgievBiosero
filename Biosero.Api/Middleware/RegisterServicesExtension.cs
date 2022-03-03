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
            services.AddTransient<BookService>();
            services.AddSingleton<BookRepository>();

            services.AddTransient<AuthenticationService>();
            services.AddTransient<UserRepository>();

            return services;
        }
    }
}
