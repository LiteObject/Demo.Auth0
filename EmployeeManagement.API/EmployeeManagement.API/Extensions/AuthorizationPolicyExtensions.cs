// File: ServiceCollectionExtensions.cs (or in an Extensions folder)
using EmployeeManagement.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace EmployeeManagement.API.Extensions
{
    public static class AuthorizationPolicyExtensions
    {
        public static IServiceCollection AddCustomAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.ReadEmployees, policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            c.Type == "scope" &&
                            c.Value.Split(' ').Contains(AuthorizationPolicies.ReadEmployees))));

                options.AddPolicy(AuthorizationPolicies.WriteEmployee, policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            c.Type == "scope" &&
                            c.Value.Split(' ').Contains(AuthorizationPolicies.WriteEmployee))));

                options.AddPolicy(AuthorizationPolicies.Admin, policy =>
                    policy.RequireAssertion(context =>
                    {
                        var roles = context.User.FindAll("localhost/roles").Select(c => c.Value);
                        return roles.Any(r => r == "Admin");
                    }));
            });

            return services;
        }
    }
}
