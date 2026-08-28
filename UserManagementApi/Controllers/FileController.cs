using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementApi.Controllers;

[ApiController]
[Authorize]
[Route("api/files")]
public class FileController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult>
        Upload(IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(
                new
                {
                    Success = false,
                    Message = "No file selected"
                });
        }

        var uploads =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Files");

        if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);

        var fileName =
            Guid.NewGuid() +
            Path.GetExtension(file.FileName);

        var fullPath =
            Path.Combine(
                uploads,
                fileName);

        using var stream =
            new FileStream(
                fullPath,
                FileMode.Create);

        await file.CopyToAsync(stream);

        return Ok(
            new
            {
                Success = true,
                FileName = fileName,
                Path = fullPath
            });
    }
}