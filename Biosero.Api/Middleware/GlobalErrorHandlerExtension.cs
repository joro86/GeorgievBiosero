using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Biosero.Api.Middleware
{
    /// <summary>
    /// Global error handler.
    /// </summary>
    public class GlobalErrorHandlerExtension
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerExtension(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                string message;

                switch (error)
                {
                    case KeyNotFoundException e:
                        Console.WriteLine(e.Message);
                        message = "Key Not Found";
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        Console.WriteLine(error.Message);
                        message = "Error occured";
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = message });
                await response.WriteAsync(result);
            }
        }
    }
}
