namespace BlazorApp1.Server.Controllers;

using BlazorApp1.Server.ViewModels.Shared;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [HttpGet, HttpPost, Route("~/error")]
    public IActionResult Error()
    {
        // If the error was not caused by an invalid
        // OIDC request, display a generic error page.
        OpenIddict.Abstractions.OpenIddictResponse? response = HttpContext.GetOpenIddictServerResponse();
        if (response is null)
            return View(new ErrorViewModel());

        return View(new ErrorViewModel
        {
            Error = response.Error,
            ErrorDescription = response.ErrorDescription
        });
    }
}
