using System.Diagnostics;

namespace CustomerManagementWebApp.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // ============================================================
        // BEFORE REQUEST
        // ============================================================

        var stopwatch = Stopwatch.StartNew();

        string method = context.Request.Method;
        string path = context.Request.Path;

        _logger.LogInformation("-------x--------x---------x--------x--------x--------x------");
        _logger.LogInformation("REQUEST STARTED: {Method} {Path}", method, path);

        // ============================================================
        // Call the NEXT middleware
        // Eventually this reaches the Controller / Endpoint.
        // ============================================================

        await _next(context);

        // ============================================================
        // AFTER REQUEST
        // ============================================================

        stopwatch.Stop();

        int statusCode = context.Response.StatusCode;

        _logger.LogInformation(
            "REQUEST FINISHED: {Method} {Path} | Status: {StatusCode} | Time: {Elapsed} ms",
            method,
            path,
            statusCode,
            stopwatch.ElapsedMilliseconds);
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }
}
