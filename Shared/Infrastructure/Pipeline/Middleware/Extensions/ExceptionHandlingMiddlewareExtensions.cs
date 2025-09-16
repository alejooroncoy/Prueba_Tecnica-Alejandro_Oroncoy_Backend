namespace GameRouletteBackend.Shared.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
/// Extensiones para configurar el middleware de manejo de excepciones
/// </summary>
public static class ExceptionHandlingMiddlewareExtensions
{
    /// <summary>
    /// Registra el middleware de manejo de excepciones en el pipeline de la aplicación
    /// </summary>
    /// <param name="app">Builder de la aplicación</param>
    /// <returns>Builder de la aplicación para encadenamiento</returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
