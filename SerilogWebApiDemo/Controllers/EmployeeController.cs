using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace SerilogWebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetEmployees()
    {
        Log.Information("GetEmployees endpoint called");

        var employees = new[]
        {
            new { EmployeeId = 1, EmployeeName = "Ali", Department = "IT", Salary = 75000 },
            new { EmployeeId = 2, EmployeeName = "Ahmed", Department = "HR", Salary = 68000 },
            new { EmployeeId = 3, EmployeeName = "Sara", Department = "Finance", Salary = 82000 }
        };

        Log.Information(
            "Returning {EmployeeCount} employees from GetEmployees",
            employees.Length);

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetEmployee(int id)
    {
        Log.Information("Searching for employee {EmployeeId}", id);

        if (id <= 0)
        {
            Log.Warning("Invalid employee id supplied: {EmployeeId}", id);
            return BadRequest("Employee ID must be greater than zero.");
        }

        var employee = new
        {
            EmployeeId = id,
            EmployeeName = "Ali",
            Department = "IT",
            Salary = 75000
        };

        Log.Information(
            "Employee {EmployeeId} found: {EmployeeName}",
            employee.EmployeeId,
            employee.EmployeeName);

        return Ok(employee);
    }

    [HttpGet("error-demo")]
    public IActionResult ErrorDemo()
    {
        try
        {
            Log.Information("Starting error-demo endpoint");

            throw new InvalidOperationException(
                "This is a sample exception for Serilog demonstration.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred in the error-demo endpoint");
            return StatusCode(500, "Sample error was logged by Serilog.");
        }
    }
}
