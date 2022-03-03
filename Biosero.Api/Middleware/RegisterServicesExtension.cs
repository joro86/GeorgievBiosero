using Biosero.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Biosero.Api.Middleware
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<BookService>();

            return services;
        }
    }
}
