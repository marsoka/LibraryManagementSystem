using FluentValidation;
using FluentValidation.AspNetCore;
using Library.DAL.Repositories.Implementations;
using Library.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IAuthorService, AuthorService>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(AuthorProfile).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();

        services.AddFluentValidationAutoValidation();

        services.AddFluentValidationClientsideAdapters();


        return services;
    }
}