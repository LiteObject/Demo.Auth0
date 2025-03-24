using EmployeeManagement.API.Authorization;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController(ILogger<EmployeesController> logger) : ControllerBase
{
    private static List<Employee> repo =
        [
            new Employee { Id = 1, Name = "Alice Johnson", Department = "Engineering", Email = "alice.johnson@example.com", Role = "User" },
            new Employee { Id = 2, Name = "Bob Brown", Department = "Project Management", Email = "bob.brown@example.com", Role = "User" },
            new Employee { Id = 3, Name = "Charlie Smith", Department = "Data Analysis", Email = "charlie.smith@example.com", Role = "User" },
            new Employee { Id = 4, Name = "Test User", Department = "Information Technology", Email = "user@test.com", Role = "User" }
        ];

    private readonly ILogger<EmployeesController> _logger = logger;

    [Authorize(Policy = AuthorizationPolicies.Admin)]
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Getting all employees");
        return Ok(repo);
    }

    [Authorize(Policy = AuthorizationPolicies.ReadEmployees)]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("Getting employee with id {id}", id);

        var userEmail = User.FindFirst("localhost/email")?.Value;
        var userRole = User.FindFirst("localhost/roles")?.Value ?? string.Empty;

        // Imperative/In-line Authorization: Ensures that a user can only access their own employee record,
        // unless they have an admin role.However, this logic is not reusable.
        var employee = repo.FirstOrDefault(e => e.Id == id 
            && (e.Email == userEmail 
                || userRole.Equals("admin", StringComparison.OrdinalIgnoreCase)));

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [Authorize(Policy = AuthorizationPolicies.WriteEmployee)]
    [HttpPost]
    public IActionResult Post(Employee employee)
    {
        _logger.LogInformation("Adding a new employee");
        employee.Id = repo.Max(e => e.Id) + 1;
        repo.Add(employee);
        return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
    }

    [HttpGet("/test")]
    public IActionResult Test()
    {
        return Ok("This is a test endpoint.");
    }
}
