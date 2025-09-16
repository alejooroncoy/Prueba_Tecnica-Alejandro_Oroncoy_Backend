namespace GameRouletteBackend.Shared.Domain.Model;

/// <summary>
/// Modelo de respuesta de error estandarizado para la API
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Mensaje de error principal
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Código de estado HTTP
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Detalles adicionales del error (opcional)
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Timestamp del error
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Ruta donde ocurrió el error
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Método HTTP de la petición
    /// </summary>
    public string? Method { get; set; }

    /// <summary>
    /// ID de correlación para tracking (opcional)
    /// </summary>
    public string? CorrelationId { get; set; }

    public ErrorResponse()
    {
    }

    public ErrorResponse(string message, int statusCode, string? details = null)
    {
        Message = message;
        StatusCode = statusCode;
        Details = details;
    }
}
