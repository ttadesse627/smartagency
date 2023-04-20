using AppDiv.CRVS.Application.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ApplicationException = AppDiv.CRVS.Application.Exceptions.ApplicationException;

namespace AppDiv.CRVS.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException e)
            {
                _logger.LogError("Validation error!");
                await HandleExceptionAsync(context, e);
            }
            catch (Exception e)
            {
                var error = e.Message;
                if (e.InnerException != null)
                    error = error + " >> " + e.InnerException.Message;
                _logger.LogError(e, error);
                await HandleExceptionAsync(context, e);
            }
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
                SqlException sqlException =>
                    exception.InnerException.Message.ToLower().Contains("with unique index") ? "Can not insert duplicate entry. The same entry already added to the database." :
                    exception.InnerException.Message.ToLower().Contains("the delete statement conflicted with the reference constraint") ? "The record you are trying to delete is being referenced by other items in the database. Please delete those items before." :
                    "Server Error",
                DbUpdateException efException =>
                    exception.InnerException.Message.ToLower().Contains("with unique index") ? "Can not insert duplicate entry. The same record is already added to the database." :
                    exception.InnerException.Message.ToLower().Contains("the delete statement conflicted with the reference constraint") ? "The record you are trying to delete is being referenced by other items in the database. Please delete those items before." :
                    "Server Error",
                ApplicationException applicationException => applicationException.Title,
                _ => "Server Error"
            };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }
            return errors;
        }
    }
}
