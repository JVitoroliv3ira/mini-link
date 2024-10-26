using Microsoft.AspNetCore.Mvc;
using MiniLink.Domain.Dtos.Responses;

namespace MiniLink.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VersionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetVersion()
    {
        return Ok(new ApiResponse<string>(true, null, "Em desenvolvimento"));
    }
}