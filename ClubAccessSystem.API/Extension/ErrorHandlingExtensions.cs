using ClubAccessSystem.API.Middleware;

namespace ClubAccessSystem.API.Extension
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
