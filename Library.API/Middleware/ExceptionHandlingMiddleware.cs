using System.Text.Json;
using Library.BLL.Exceptions;
using Library.BLL.Responses;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            await HandleAppExceptionAsync(context, ex);
        }
        catch (Exception)
        {
            await HandleInternalServerErrorAsync(context);
        }
    }

    private static async Task HandleAppExceptionAsync(
        HttpContext context,
        AppException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;

        var response = new ErrorResponse
        {
            Success = false,
            StatusCode = exception.StatusCode,
            Message = exception.Message,
            Errors = null
        };

        await context.Response.WriteAsJsonAsync(response);
    }

    private static async Task HandleInternalServerErrorAsync(
        HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        var response = new ErrorResponse
        {
            Success = false,
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "An unexpected error occurred.",
            Errors = null
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}