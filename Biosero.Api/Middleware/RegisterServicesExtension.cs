using Biosero.Api.Utilities;
using Biosero.Data.Repositories;
using Biosero.Service.Interfaces;
using Biosero.Service.Models.Api;
using Biosero.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Biosero.Api.Middleware
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserContext>(provider => {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                return new ApiUserContext(httpContextAccessor.HttpContext.User);
            });

            services.AddTransient<BookService>();
            services.AddSingleton<BookRepository>();

            services.AddScoped<IJwtHandler, JwtHandler>();

            services.AddTransient<AuthenticationService>();
            services.AddSingleton<UserRepository>();

            return services;
        }
    }
}
