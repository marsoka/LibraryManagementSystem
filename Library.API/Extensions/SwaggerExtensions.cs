namespace Library.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerServices(
        this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddOpenApi();

        return services;
    }
}