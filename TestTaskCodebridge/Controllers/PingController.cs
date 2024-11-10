using Microsoft.AspNetCore.Mvc;

namespace TestTaskCodebridge.Controllers;

[ApiController]
[Route("ping")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Dogshouseservice.Version1.0.1");
    }
}
