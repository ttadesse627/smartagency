namespace AppDiv.CRVS.Api.Middleware
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

