using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Middlewares
{
    public static class SessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseLanguageMiddleware(this IApplicationBuilder app)
        {
            app.Use(async (httpContext, next) =>
            {
                httpContext.Session.LoadAsync().Wait();

                // Create a default session key-value pair if not present
                var key = "Language";
                if (string.IsNullOrEmpty(httpContext.Session.GetString(key)))
                {
                    httpContext.Session.SetString(key, "BG");
                }

                // Call the next middleware in the pipeline
                await next(httpContext);
            });

            return app;
        }
    }
}
