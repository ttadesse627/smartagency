namespace AppDiv.SmartAgency.API.Middleware
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

