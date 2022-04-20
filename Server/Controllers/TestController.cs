namespace LegendaryTelegram.Server.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OpenIddict.Validation.AspNetCore;

[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet]
    public IActionResult Get() => Ok("Hello, world!");
}
