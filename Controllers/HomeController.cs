using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok("Ola mundo!");
    }
}