namespace EmployeeManagement.API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Department { get; set; }
        public required string Email { get; set; }
    }
}
