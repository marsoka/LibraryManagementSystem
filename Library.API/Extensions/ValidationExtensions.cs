using Library.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidationResponses(
        this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new ErrorResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Validation failed.",
                    Errors = errors
                };

                return new BadRequestObjectResult(response);
            };
        });

        return services;
    }
}