using LegendaryTelegram.Server.ViewModels.Shared;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryTelegram.Server;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    [HttpGet, HttpPost, Route("~/error")]
    public IActionResult Error()
    {
        // If the error was not caused by an invalid
        // OIDC request, display a generic error page.
        var response = HttpContext.GetOpenIddictServerResponse();
        if (response is null) return View(new ErrorViewModel());

        return View(new ErrorViewModel { Error = response.Error, ErrorDescription = response.ErrorDescription });
    }
}

