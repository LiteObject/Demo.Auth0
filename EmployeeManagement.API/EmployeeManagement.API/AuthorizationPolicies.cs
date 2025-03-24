// File: AuthorizationPolicies.cs
namespace EmployeeManagement.API.Authorization
{
    public static class AuthorizationPolicies
    {
        public const string ReadEmployees = "read:employees";
        public const string WriteEmployee = "write:employee";
        public const string Admin = "admin";
    }
}
