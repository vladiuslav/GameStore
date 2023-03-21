using Serilog;

namespace GameStore.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly Serilog.ILogger _logger;
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next,IHostEnvironment env) {
            _next=next;

            var logFilePath = Path.Combine(env.ContentRootPath, "logs/app-.log");
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(
                path: logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: null,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
            .CreateLogger();

            _logger =Log.Logger;
            
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.ToString());
                context.Response.StatusCode = 500;
            }
        }
    }
}
