namespace Library.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApplicationMiddlewares(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}