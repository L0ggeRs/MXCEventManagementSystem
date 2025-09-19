using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Net;

namespace MXC.WebApi.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    private readonly string _applicationJsonRoute = "application/json";
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BadHttpRequestException ex)
        {
            _logger.LogError(ex, "Bad request error occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = _applicationJsonRoute;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = "Bad request error occurred.",
                Details = ex.Message
            }));
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Request error occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = _applicationJsonRoute;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = "Request error occurred.",
                Details = ex.Message
            }));
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Unauthorized access error occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = _applicationJsonRoute;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = "Unauthorized access error occurred.",
                Details = ex.Message
            }));
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Argument error occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = _applicationJsonRoute;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = "Argument error occurred.",
                Details = ex.Message
            }));
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "SQL Exception occured.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = _applicationJsonRoute;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = "Database error occurred.",
                Details = ex.Message
            }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = _applicationJsonRoute;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = "An unexpected error occurred.",
                Details = ex.Message
            }));
        }
    }
}