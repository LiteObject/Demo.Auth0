
using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Add health checks
            builder.Services.AddHealthChecks();

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Configure JWT authentication
            var auth0Domain = builder.Configuration["Auth0:Domain"];
            var auth0Audience = builder.Configuration["Auth0:Audience"];

            // Configure the default authentication scheme to use JWT Bearer tokens.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{auth0Domain}/";
                options.Audience = auth0Audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://{auth0Domain}/",
                    ValidateAudience = true,
                    ValidAudience = auth0Audience,
                    ValidateLifetime = true                    
                };
            });

            // Use the custom extension to add authorization policies
            builder.Services.AddCustomAuthorizationPolicies();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Register custom middleware
            app.UseMiddleware<TokenLogger>();

            // Ensure the authentication middleware is used
            app.UseAuthentication();            
            app.UseAuthorization();

            // Map health check endpoint
            app.MapHealthChecks("/health");

            app.MapControllers();

            app.Run();
        }
    }
}
