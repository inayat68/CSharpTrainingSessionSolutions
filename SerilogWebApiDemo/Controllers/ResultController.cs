using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogWebApiDemo.Models;

namespace SerilogWebApiDemo.Controllers;

//.NET Sample Codes
//https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main

//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/
//https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types

//Tutorial: Create a controller-based web API with ASP.NET Core
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-10.0&tabs=visual-studio

//https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/mvc/action-return-types/
//https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/mvc/action-return-types/8.x/WebApiSample/Controllers/ActionResultProductsController.cs

//[ProducesResponseType(StatusCodes.Status200OK)]
//Those attributes are added to describe the possible HTTP responses of the API endpoint, mainly for Swagger/OpenAPI documentation.

[ApiController]
[Route("api/[controller]")]
public class ResultController : ControllerBase
{

    // --------------------------------------------------------
    // 200 OK
    // --------------------------------------------------------
    [HttpGet("ok")]
    public IActionResult GetOk()
    {
        Log.Information("--------------------------------------------------");
        Log.Information("Returning OK with string only");
        Log.Information("--------------------------------------------------");

        //ContentResult Content(string content);
        ContentResult resultContent = new ContentResult
        {
            Content = "Employee retrieved successfully."
        };
        //return Content("Employee retrieved successfully.");

        //return Ok();
        return Ok("Employee retrieved successfully.");

        //Mouseover above OK() method and click OkObjectResult : ObjectResult : ActionResult, IStatusCodeActionResult, IActionResult
        //OkObjectResult result = new OkObjectResult("Employee retrieved successfully.");
        //return result;
    }


    // --------------------------------------------------------
    // 200 OK + Object
    // --------------------------------------------------------
    [HttpGet("employee")]
    public IActionResult GetEmployee()
    {
        var employee = new
        {
            Id = 101,
            Name = "Ali",
            Department = "IT"
        };

        return Ok(employee);
    }


    // --------------------------------------------------------
    // 404 Not Found
    // --------------------------------------------------------
    [HttpGet("notfound")]
    public IActionResult GetNotFound()
    {
        return NotFound("Employee was not found.");
    }


    // --------------------------------------------------------
    // 400 Bad Request
    // --------------------------------------------------------
    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("Invalid employee information.");
    }


    // --------------------------------------------------------
    // 401 Unauthorized
    // --------------------------------------------------------
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized("Authentication is required.");
    }


    // --------------------------------------------------------
    // 403 Forbidden
    // --------------------------------------------------------
    [HttpGet("forbidden")]
    public IActionResult GetForbidden()
    {
        return Forbid();
    }


    // --------------------------------------------------------
    // 204 No Content
    // --------------------------------------------------------
    [HttpGet("nocontent")]
    public IActionResult GetNoContent()
    {
        return NoContent();
    }


    // --------------------------------------------------------
    // 201 Created
    // --------------------------------------------------------
    [HttpPost("create")]
    public IActionResult CreateEmployee()
    {
        var employee = new
        {
            Id = 105,
            Name = "Sara",
            Department = "Finance"
        };

        return Created("/api/Employee/105", employee);
    }


    // --------------------------------------------------------
    // 201 CreatedAtAction
    // --------------------------------------------------------
    [HttpPost("create2")]
    public IActionResult CreateEmployee2()
    {
        var employee = new
        {
            Id = 106,
            Name = "Ahmed",
            Department = "HR"
        };

        return CreatedAtAction(
            nameof(GetEmployeeById),
            new { id = employee.Id },
            employee);
    }


    // --------------------------------------------------------
    // 200 OK / 404 Not Found based on condition
    // --------------------------------------------------------
    [HttpGet("{id:int}")]
    public IActionResult GetEmployeeById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Employee ID must be greater than zero.");
        }

        if (id != 101)
        {
            return NotFound($"Employee {id} was not found.");
        }

        var employee = new
        {
            Id = 101,
            Name = "Ali",
            Department = "IT"
        };

        return Ok(employee);
    }


    // --------------------------------------------------------
    // 409 Conflict
    // --------------------------------------------------------
    [HttpPost("conflict")]
    public IActionResult ConflictExample()
    {
        return Conflict("Employee already exists.");
    }


    // --------------------------------------------------------
    // 405 Method Not Allowed
    // --------------------------------------------------------
    [HttpGet("method-not-allowed")]
    public IActionResult MethodNotAllowed()
    {
        return StatusCode(StatusCodes.Status405MethodNotAllowed, "This operation is not allowed.");
    }

    // --------------------------------------------------------
    // 500 Internal Server Error
    // --------------------------------------------------------
    [HttpGet("error")]
    public IActionResult Error()
    {
        return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
    }


    // --------------------------------------------------------
    // 200 OK + 404 Not Found
    // Demonstrates ProducesResponseType
    // --------------------------------------------------------
    [HttpGet("find/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult FindEmployee(int id)
    {
        if (id == 101)
        {
            var employee = new
            {
                Id = 101,
                Name = "Ali",
                Department = "IT"
            };

            return Ok(employee);
        }

        return NotFound($"Employee {id} was not found.");
    }

    public class EmployeeRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    // --------------------------------------------------------
    // 200 OK + 400 Bad Request + 404 Not Found
    // Demonstrates multiple ProducesResponseType attributes
    // --------------------------------------------------------
    [HttpGet("search/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult SearchEmployee(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Employee ID must be greater than zero.");
        }

        if (id != 101)
        {
            return NotFound($"Employee {id} was not found.");
        }

        var employee = new
        {
            Id = 101,
            Name = "Ali",
            Department = "IT"
        };

        return Ok(employee);
    }



    // --------------------------------------------------------
    // Async POST - Similar to Microsoft/sample application
    // --------------------------------------------------------
    [HttpPost("process", Name = "ProcessEmployee")]
    [ProducesResponseType<EmployeeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<EmployeeResponse>> ProcessEmployee([FromBody] EmployeeRequest request, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Employee name is required.");
            }

            // Simulate asynchronous processing
            await Task.Delay(500, ct);

            var response = new EmployeeResponse
            {
                Id = 108,
                Name = request.Name,
                Department = request.Department
            };

            return Ok(response);
        }
        catch (OperationCanceledException)
        {
            return BadRequest("Operation was cancelled.");
        }
    }


}



// ┌──────────────────────────┬──────────────┬──────────────────────────────────────────┐
// │ C# Method                │ HTTP Status  │ Typical Meaning                          │
// ├──────────────────────────┼──────────────┼──────────────────────────────────────────┤
// │ Ok()                     │ 200          │ Successful request                       │
// │ Ok(data)                 │ 200          │ Success + data                           │
// │ Created()                │ 201          │ Resource created                         │
// │ CreatedAtAction()        │ 201          │ Created + URL to resource                │
// │ NoContent()              │ 204          │ Success, no response body                │
// │ BadRequest()             │ 400          │ Invalid request                          │
// │ Unauthorized()           │ 401          │ Authentication required                  │
// │ Forbid()                 │ 403          │ Authenticated but not allowed            │
// │ NotFound()               │ 404          │ Resource doesn't exist                   │
// │ Conflict()               │ 409          │ Request conflicts with existing data     │
// │ StatusCode(500)          │ 500          │ Server error                             │
// └──────────────────────────┴──────────────┴──────────────────────────────────────────┘

// ==============================================================================================================================
// Common ControllerBase Result Helpers
// ==============================================================================================================================
//
// Helper Method                  Result Class                    HTTP     Purpose                              JavaScript fetch()
//                                                                 Status                                      Result
// ------------------------------------------------------------------------------------------------------------------------------
// Ok()                            OkResult                        200      Success, no body                    response.status === 200
//
// Ok(value)                       OkObjectResult                  200      Success + data                     response.json()
//
// Created(uri, value)             CreatedResult                   201      Resource created                  response.status === 201
//
// CreatedAtAction(...)            CreatedAtActionResult           201      Created + action URL               response.status === 201
//
// CreatedAtRoute(...)             CreatedAtRouteResult            201      Created + route URL                response.status === 201
//
// NoContent()                     NoContentResult                 204      Success, no body                   response.status === 204
//
// BadRequest()                    BadRequestResult                400      Invalid request                    response.status === 400
//
// BadRequest(value)               BadRequestObjectResult          400      Error + details                    response.json()
//
// Unauthorized()                  UnauthorizedResult              401      Authentication failed              response.status === 401
//
// Forbid()                        ForbidResult                    403      Authenticated but not allowed      response.status === 403
//
// NotFound()                      NotFoundResult                  404      Resource not found                response.status === 404
//
// NotFound(value)                 NotFoundObjectResult            404      Not found + details               response.json()
//
// Conflict()                      ConflictResult                  409      Resource/state conflict           response.status === 409
//
// Conflict(value)                 ConflictObjectResult            409      Conflict + details                response.json()
//
// UnprocessableEntity()           UnprocessableEntityResult       422      Validation/semantic error         response.status === 422
//
// StatusCode(500)                 StatusCodeResult                Custom   Custom HTTP status                response.status === 500
//
// StatusCode(500, value)          ObjectResult                    Custom   Custom status + response body     response.json()
//
// Content(text)                   ContentResult                   200      Return text/content                response.text()
//
// Content(text, type)             ContentResult                   200      Text + specified content type     response.text()
//
// File(...)                       FileResult                      200      Return a file                     response.blob()
//
// PhysicalFile(...)               PhysicalFileResult              200      Return physical file              response.blob()
//
// VirtualFile(...)                VirtualFileResult               200      Return virtual file               response.blob()
//
// File(byte[], contentType)       FileContentResult               200      File from byte array              response.blob()
//
// File(Stream, contentType)       FileStreamResult                200      File from stream                  response.blob()
//
// Problem()                       ProblemDetails response         500      Standard API error                response.json()
//
// ValidationProblem()             ValidationProblemDetails        400      Validation errors                 response.json()
//
// ==============================================================================================================================