using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("RR")]
public class BbController(
    
    ILogger<BbController> logger
    ):ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        logger.LogWarning("Too Hot");
        return Ok("RRRR");
    }
}