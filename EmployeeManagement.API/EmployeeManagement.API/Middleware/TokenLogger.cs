namespace EmployeeManagement.API.Middleware
{
    public class TokenLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenLogger> _logger;

        public TokenLogger(RequestDelegate next, ILogger<TokenLogger> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString();
                _logger.LogInformation("Authorization Token: {Token}", token);
            }

            await _next(context);
        }
    }
}
