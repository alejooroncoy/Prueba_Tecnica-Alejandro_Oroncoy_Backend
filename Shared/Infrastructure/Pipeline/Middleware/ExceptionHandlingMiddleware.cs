using System.Net;
using System.Text.Json;
using GameRouletteBackend.Shared.Domain.Model;

namespace GameRouletteBackend.Shared.Infrastructure.Pipeline.Middleware;

/// <summary>
/// Middleware para manejo global de excepciones y devolución de errores en formato JSON
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Una excepción no controlada ocurrió durante el procesamiento de la petición");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = CreateErrorResponse(context, exception);
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;

        var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(jsonResponse);
    }

    private static ErrorResponse CreateErrorResponse(HttpContext context, Exception exception)
    {
        var errorResponse = new ErrorResponse
        {
            Path = context.Request.Path,
            Method = context.Request.Method,
            CorrelationId = context.TraceIdentifier
        };

        switch (exception)
        {
            case ArgumentNullException nullEx:
                errorResponse.Message = "Parámetro requerido faltante: " + nullEx.ParamName;
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Details = "A value is required for this parameter";
                break;

            case ArgumentException argEx:
                errorResponse.Message = "Error de validación: " + argEx.Message;
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Details = "Los parámetros proporcionados no son válidos";
                break;

            case InvalidOperationException invalidOpEx:
                errorResponse.Message = "Operación no válida: " + invalidOpEx.Message;
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Details = "The requested operation cannot be performed in the current state";
                break;

            case UnauthorizedAccessException:
                errorResponse.Message = "Acceso no autorizado";
                errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Details = "No tienes permisos para realizar esta acción";
                break;

            case KeyNotFoundException:
                errorResponse.Message = "Recurso no encontrado";
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Details = "El recurso solicitado no existe";
                break;

            case TimeoutException:
                errorResponse.Message = "Tiempo de espera agotado";
                errorResponse.StatusCode = (int)HttpStatusCode.RequestTimeout;
                errorResponse.Details = "La operación tardó demasiado tiempo en completarse";
                break;

            case NotImplementedException:
                errorResponse.Message = "Funcionalidad no implementada";
                errorResponse.StatusCode = (int)HttpStatusCode.NotImplemented;
                errorResponse.Details = "Esta funcionalidad aún no está disponible";
                break;

            default:
                errorResponse.Message = "Error interno del servidor";
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Details = "Ha ocurrido un error inesperado. Por favor, inténtalo de nuevo más tarde";
                break;
        }

        return errorResponse;
    }
}
