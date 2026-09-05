
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    public TestController()
    {
    }

    // ============================================================
    // 1. [HttpGet] - Handles HTTP GET requests.
    // ============================================================

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("This is HTTP GET");
    }


    // ============================================================
    // 2. [HttpGet("message")] - Handles HTTP GET requests.
    // ============================================================

    [HttpGet("message")]
    public IActionResult Message()
    {
        return Ok("This is GET /api/test/message");
    }


    // ============================================================
    // 3. [HttpGet("{id}")]
    // ============================================================

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok($"Requested ID = {id}");
    }


    // ============================================================
    // 4. [HttpGet("search")] + [FromQuery]
    // ============================================================

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string name)
    {
        return Ok($"Searching for: {name}");
    }


    // ============================================================
    // 5. [HttpPost] - Handles HTTP POST requests.
    // ============================================================

    [HttpPost]
    public IActionResult Post()
    {
        return Ok("This is HTTP POST");
    }


    // ============================================================
    // 6. [HttpPost("create")] - Handles HTTP POST requests.
    // ============================================================

    [HttpPost("create")]
    public IActionResult Create([FromBody] TestRequest request)
    {
        return Ok(new
        {
            Message = "User created",
            request.Name,
            request.Age
        });
    }


    // ============================================================
    // 7. [HttpPut("{id}")] - Handles HTTP PUT requests.
    // Updates or replaces an existing resource with the supplied data.
    // Usually the client sends the complete updated resource.
    // ============================================================

    [HttpPut("{id}")]
    public IActionResult Update(
    int id,
    [FromBody] TestRequest request)
    {
        return Ok(new
        {
            Message = "User updated",
            Id = id,
            request.Name,
            request.Age
        });
    }


    // ============================================================
    // 8. [HttpPatch("{id}")] - Handles HTTP PATCH requests.
    // Partially updates an existing resource by changing only selected fields.
    // Usually the client sends only the properties that need to be changed.
    // ============================================================

    [HttpPatch("{id}")]
    public IActionResult Patch(int id)
    {
        return Ok($"PATCH request for ID = {id}");
    }

    // PATCH with a specific field
    // Example: PATCH /api/test/10/name

    [HttpPatch("{id}/name")]
    public IActionResult PatchName(
        int id,
        [FromBody] string name)
    {
        return Ok(new
        {
            Message = "Name updated",
            Id = id,
            Name = name
        });
    }


    // PATCH with route + query parameter + body
    // Example: PATCH /api/test/10/status?notify=true

    [HttpPatch("{id}/status")]
    public IActionResult PatchStatus(
        [FromRoute] int id,
        [FromQuery] bool notify,
        [FromBody] string status)
    {
        return Ok(new
        {
            Message = "Status updated",
            Id = id,
            Status = status,
            Notify = notify
        });
    }


    // ============================================================
    // 9. [HttpDelete("{id}")]
    // ============================================================

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok($"Deleted ID = {id}");
    }


    // ============================================================
    // 10. [HttpHead] - Handles HTTP HEAD requests.
    // ============================================================

    [HttpHead("check")]
    public IActionResult Head()
    {
        return Ok();
    }


    // ============================================================
    // 11. [HttpOptions] - Handles HTTP OPTIONS requests.
    // ============================================================

    [HttpOptions("options")]
    public IActionResult Options()
    {
        Response.Headers.Append("Allow", "GET, POST, PUT, DELETE");

        return Ok();
    }


    // ============================================================
    // 12. [NonAction] -  Marks a public controller method as NOT being an API action.
    // ============================================================

    [NonAction]
    public string HelperMethod()
    {
        return "This method is NOT an API endpoint";
    }


    // ============================================================
    // 13. [ActionName]
    // ============================================================

    [HttpGet("action-name")]
    [ActionName("GetSpecialData")]
    public IActionResult SomeOtherMethodName()
    {
        return Ok("C# method name is SomeOtherMethodName");
    }


    // ============================================================
    // 14. [FromRoute]
    // ============================================================

    [HttpGet("route/{id}")]
    public IActionResult FromRouteExample(
        [FromRoute] int id)
    {
        return Ok($"ID from route = {id}");
    }


    // ============================================================
    // 15. [FromQuery]
    // ============================================================

    [HttpGet("query")]
    public IActionResult FromQueryExample(
        [FromQuery] string name,
        [FromQuery] int age)
    {
        return Ok(new
        {
            Name = name,
            Age = age
        });
    }


    // ============================================================
    // 16. [FromHeader]
    // ============================================================

    [HttpGet("header")]
    public IActionResult FromHeaderExample(
        [FromHeader(Name = "X-Test-Header")] string value)
    {
        return Ok($"Header value = {value}");
    }


    // ============================================================
    // 17. [FromBody]
    // ============================================================

    [HttpPost("body")]
    public IActionResult FromBodyExample(
        [FromBody] TestRequest request)
    {
        return Ok(request);
    }


    // ============================================================
    // 18. [AllowAnonymous]
    // ============================================================

    [AllowAnonymous]
    [HttpGet("anonymous")]
    public IActionResult Anonymous()
    {
        return Ok("This endpoint allows anonymous access");
    }


    // ============================================================
    // 19. [Authorize]
    // ============================================================

    [Authorize]
    [HttpGet("secure")]
    public IActionResult Secure()
    {
        return Ok("You are authenticated");
    }


    // ============================================================
    // 20. [Authorize(Roles = "Admin")]
    // ============================================================

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return Ok("You have Admin role");
    }


    // ============================================================
    // 21. [ApiExplorerSettings(IgnoreApi = true)]
    // ============================================================

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("hidden")]
    public IActionResult HiddenFromSwagger()
    {
        return Ok("This endpoint is hidden from Swagger");
    }


    // ============================================================
    // 22. [Produces]
    // ============================================================

    [Produces("application/json")]
    [HttpGet("json")]
    public IActionResult JsonResponse()
    {
        return Ok(new
        {
            Message = "JSON response"
        });
    }


    // ============================================================
    // 23. [Consumes]
    // ============================================================

    [Consumes("application/json")]
    [HttpPost("consume-json")]
    public IActionResult ConsumeJson(
        [FromBody] TestRequest request)
    {
        return Ok(request);
    }


    // ============================================================
    // 24. [ProducesResponseType]
    // ============================================================

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("response-types/{id}")]
    public IActionResult ResponseTypes(int id)
    {
        if (id <= 0)
            return NotFound();

        return Ok($"ID = {id}");
    }


    // ============================================================
    // 25. [RequestSizeLimit]
    // ============================================================

    [RequestSizeLimit(10_000_000)]
    [HttpPost("upload")]
    public IActionResult Upload()
    {
        return Ok("Request size can be up to 10 MB");
    }


    // ============================================================
    // 26. Exception Test
    // ============================================================

    [HttpGet("error")]
    public async Task<IActionResult> ErrorAsync()
    {
        await Task.Delay(10);

        throw new Exception("TEST ERROR");

        //return Ok("This line will never execute");
    }
}


// ================================================================
// Request DTO used by POST / PUT examples
// ================================================================

public class TestRequest
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
}



/*
=========================================================================================================
 ASP.NET CORE CONTROLLER / ACTION ATTRIBUTES - QUICK REFERENCE
=========================================================================================================

 Attribute                         Sample / Overload                         Short Description
---------------------------------------------------------------------------------------------------------

 [HttpGet]                         [HttpGet]                                  Handles HTTP GET requests.
                                   [HttpGet("users")]                          GET /users
                                   [HttpGet("{id}")]                           GET /5
                                   [HttpGet("users/{id}")]                     GET /users/5

 [HttpPost]                        [HttpPost]                                 Handles HTTP POST requests.
                                   [HttpPost("users")]                         POST /users
                                   [HttpPost("{id}")]                          POST /5

 [HttpPut]                         [HttpPut]                                  Handles HTTP PUT requests.
                                   [HttpPut("users/{id}")]                    PUT /users/5

 [HttpPatch]                       [HttpPatch]                                Handles HTTP PATCH requests.
                                   [HttpPatch("users/{id}")]                  PATCH /users/5

 [HttpDelete]                      [HttpDelete]                               Handles HTTP DELETE requests.
                                   [HttpDelete("{id}")]                        DELETE /5

 [HttpHead]                        [HttpHead]                                 Handles HTTP HEAD requests.
                                   [HttpHead("users")]                         HEAD /users

 [HttpOptions]                     [HttpOptions]                              Handles HTTP OPTIONS requests.
                                   [HttpOptions("users")]                      OPTIONS /users


 [Route]                            [Route("api/users")]                        Defines the URL route.
                                   [Route("api/users/{id}")]                    Can contain route parameters.

                                   Example:
                                   [Route("api/users")]
                                   public class UserController : ControllerBase


 [ApiController]                    [ApiController]                             Enables API-specific behavior.
                                                                                 Automatic model validation,
                                                                                 binding improvements, etc.


 [NonAction]                        [NonAction]                                 Marks a public controller method
                                   public IActionResult Helper()                as NOT being an API action.

                                   Useful for helper methods inside controllers.


 [ActionName]                       [ActionName("GetUser")]                     Changes the action name used
                                   public IActionResult Find()                  by routing/action selection.

                                   C# method: Find()
                                   Action name: GetUser


 [ApiExplorerSettings]              [ApiExplorerSettings(IgnoreApi = true)]     Controls API Explorer/Swagger
                                                                                 visibility.

                                   [ApiExplorerSettings(GroupName = "v1")]      Places API in a Swagger group.


 [Produces]                         [Produces("application/json")]              Specifies response content type.

                                   [Produces("application/json",
                                              "text/plain")]


 [Consumes]                         [Consumes("application/json")]              Specifies accepted request
                                                                                 content types.

                                   [Consumes("application/json",
                                             "application/xml")]


 [ProducesResponseType]             [ProducesResponseType(200)]                Documents possible response
                                   [ProducesResponseType(404)]                 status codes for Swagger.

                                   [ProducesResponseType(
                                       typeof(UserDto), 200)]


 [FromRoute]                        [FromRoute]                                Gets a value from the URL route.

                                   public IActionResult Get(
                                       [FromRoute] int id)

                                   GET /api/users/10


 [FromQuery]                        [FromQuery]                                Gets a value from the query string.

                                   public IActionResult Get(
                                       [FromQuery] int id)

                                   GET /api/users?id=10


 [FromBody]                         [FromBody]                                 Gets an object from the HTTP
                                                                                 request body.

                                   public IActionResult Create(
                                       [FromBody] UserDto user)


 [FromHeader]                       [FromHeader]                               Gets a value from an HTTP header.

                                   public IActionResult Get(
                                       [FromHeader] string token)


 [FromForm]                         [FromForm]                                 Gets values from form-data.

                                   public IActionResult Upload(
                                       [FromForm] IFormFile file)


 [FromServices]                     [FromServices]                             Gets a service directly from
                                   [FromServices] MyService service             Dependency Injection.


 [Authorize]                        [Authorize]                                Requires authentication.

                                   [Authorize(Roles = "Admin")]                Requires a specific role.

                                   [Authorize(Policy = "AdminPolicy")]         Requires an authorization
                                                                                 policy.


 [AllowAnonymous]                   [AllowAnonymous]                            Allows access without
                                                                                 authentication.

                                   Usually used on Login/Register actions.


 [ValidateAntiForgeryToken]         [ValidateAntiForgeryToken]                  Validates an anti-forgery token.
                                                                                 Mainly used with forms/browser
                                                                                 applications.


 [IgnoreAntiforgeryToken]           [IgnoreAntiforgeryToken]                    Disables anti-forgery validation
                                                                                 for an action/controller.


 [AutoValidateAntiforgeryToken]    [AutoValidateAntiforgeryToken]               Automatically validates
                                                                                 anti-forgery tokens for
                                                                                 unsafe HTTP methods.


 [ResponseCache]                    [ResponseCache(Duration = 60)]              Controls HTTP response caching.

                                   [ResponseCache(
                                       NoStore = true)]

                                   [ResponseCache(
                                       Location = ResponseCacheLocation.None)]


 [RequestSizeLimit]                 [RequestSizeLimit(10_000_000)]              Sets maximum request size
                                                                                 in bytes.

                                   10 MB = 10_000_000 bytes


 [RequestFormLimits]                [RequestFormLimits(
                                       MultipartBodyLengthLimit =
                                       10_000_000)]                              Controls multipart/form-data
                                                                                 request limits.


 [ApiConventionMethod]              [ApiConventionMethod(
                                       typeof(DefaultApiConventions),
                                       nameof(DefaultApiConventions.Get))]       Helps Swagger/API Explorer
                                                                                 infer API conventions.


 [ApiConventionType]                [ApiConventionType(
                                       typeof(DefaultApiConventions))]           Applies API conventions to
                                                                                 controller actions.


 [Area]                             [Area("Admin")]                             Defines an MVC area.

                                   URL routing can be organized as:
                                   Admin/Users
                                   Admin/Roles
                                   etc.


=========================================================================================================
 ROUTING ATTRIBUTE EXAMPLES
=========================================================================================================

 [Route("api/users")]
 public class UserController : ControllerBase

 [HttpGet]
 public IActionResult Get()

 Result:
 GET /api/users


 [HttpGet("{id}")]
 public IActionResult Get(int id)

 Result:
 GET /api/users/10


 [HttpGet("search")]
 public IActionResult Search([FromQuery] string name)

 Result:
 GET /api/users/search?name=John


 [HttpPost]
 public IActionResult Create([FromBody] UserDto user)

 Result:
 POST /api/users


 [HttpPut("{id}")]
 public IActionResult Update(int id, [FromBody] UserDto user)

 Result:
 PUT /api/users/10


 [HttpDelete("{id}")]
 public IActionResult Delete(int id)

 Result:
 DELETE /api/users/10


=========================================================================================================
 IMPORTANT DIFFERENCE
=========================================================================================================

 [HttpGet]
     -> Defines an HTTP GET action.

 [NonAction]
     -> Explicitly tells ASP.NET Core that the method is NOT an API action.

 [Route]
     -> Defines the URL route.

 [ApiController]
     -> Enables API controller behavior.

 [ApiExplorerSettings(IgnoreApi = true)]
     -> Hides the controller/action from Swagger/API Explorer.

 [Authorize]
     -> Requires authentication/authorization.

 [AllowAnonymous]
     -> Allows unauthenticated access.

 [FromRoute]
     -> Gets data from URL route.

 [FromQuery]
     -> Gets data from query string.

 [FromBody]
     -> Gets data from request body.

=========================================================================================================
*/