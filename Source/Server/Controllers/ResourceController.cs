using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

using static OpenIddict.Abstractions.OpenIddictConstants;

namespace LegendaryTelegram.Server.Controllers;

[Route("api")]
public class ResourceController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;

    public ResourceController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("message")]
    public async Task<IActionResult> GetMessage()
    {
        // This demo action requires that the client application be granted the "demo_api" scope.
        // If it was not granted, a detailed error is returned to the client application to inform it
        // that the authorization process must be restarted with the specified scope to access this API.
        if (!User.HasScope("demo_api"))
        {
            return Forbid(
                new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictValidationAspNetCoreConstants.Properties.Scope] = "demo_api",
                    [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InsufficientScope,
                    [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                        "The 'demo_api' scope is required to perform this action."
                }),
                OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        }

        var user = await this.userManager.FindByIdAsync(User.GetClaim(Claims.Subject));
        if (user is null)
        {
            return Challenge(
                new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                    [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                        "The specified access token is bound to an account that no longer exists."
                }),
                OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        }

        return Content($"{user.UserName} has been successfully authenticated.");
    }
}
