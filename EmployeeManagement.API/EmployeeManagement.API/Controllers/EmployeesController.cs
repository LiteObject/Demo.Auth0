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
            new Employee { Id = 1, Name = "Alice Johnson", Department = "Engineering", Email = "alice.johnson@example.com" },
            new Employee { Id = 2, Name = "Bob Brown", Department = "Project Management", Email = "bob.brown@example.com" },
            new Employee { Id = 3, Name = "Charlie Smith", Department = "Data Analysis", Email = "charlie.smith@example.com" }
        ];

    private readonly ILogger<EmployeesController> _logger = logger;

    [Authorize(Policy = "read:employees")]
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Getting all employees");
        return Ok(repo);
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("Getting employee with id {id}", id);
        var employee = repo.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [Authorize(Policy = "write:employee")]
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
