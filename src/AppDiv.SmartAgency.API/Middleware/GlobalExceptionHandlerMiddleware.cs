


using System.Text.Json;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Utility.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ApplicationException = AppDiv.SmartAgency.Utility.Exceptions.AppException;

namespace AppDiv.SmartAgency.API.Middleware;
public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger) : IExceptionHandler
{
    private readonly RequestDelegate _next = next;
    public ILogger<GlobalExceptionHandlerMiddleware> _logger { get; } = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception)
        {

            throw;
        }


        return true;
    }
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,

            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            SqlException =>
                exception.InnerException is not null ? (exception.InnerException.Message.ToLower().Contains("with unique index") ? "Can not insert duplicate entry. The same entry already added to the database." :
                exception.InnerException.Message.ToLower().Contains("the delete statement conflicted with the reference constraint") ? "The record you are trying to delete is being referenced by other items in the database. Please delete those items before." :
                "Server Error") : "Server Error",
            DbUpdateException =>
                exception.InnerException is not null ? (exception.InnerException.Message.ToLower().Contains("with unique index") ? "Can not insert duplicate entry. The same record is already added to the database." :
                exception.InnerException.Message.ToLower().Contains("the delete statement conflicted with the reference constraint") ? "The record you are trying to delete is being referenced by other items in the database. Please delete those items before." :
                "Server Error") : "Server Error",
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null!;
        if (exception is ValidationException validationException)
        {
            errors = validationException.ErrorsDictionary;
        }
        return errors;
    }
}