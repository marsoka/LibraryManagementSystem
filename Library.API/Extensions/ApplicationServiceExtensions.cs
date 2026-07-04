using FluentValidation;
using FluentValidation.AspNetCore;
using Library.BLL.Interfaces;
using Library.BLL.Services;
using Library.DAL.Repositories.Implementations;
using Library.DAL.Repositories.Interfaces;
using Library.Domain;
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

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IPublisherRepository, PublisherRepository>();

        services.AddScoped<IPublisherService, PublisherService>();

        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<IBookService, BookService>();

        services.AddScoped<IMemberRepository, MemberRepository>();

        services.AddScoped<IMemberService, MemberService>();

        services.AddScoped<IBorrowingRepository, BorrowingRepository>();

        services.AddScoped<IBorrowingService, BorrowingService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(AuthorProfile).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();

        services.AddFluentValidationAutoValidation();

        services.AddFluentValidationClientsideAdapters();

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        return services;
    }
}