﻿using Biosero.Api.Utilities;
using Biosero.Data.Repositories;
using Biosero.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Biosero.Api.Middleware
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<BookService>();
            services.AddSingleton<BookRepository>();

            services.AddScoped<JwtHandler>();

            services.AddTransient<AuthenticationService>();
            services.AddSingleton<UserRepository>();

            return services;
        }
    }
}
