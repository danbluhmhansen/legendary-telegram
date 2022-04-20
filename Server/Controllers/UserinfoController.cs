namespace LegendaryTelegram.Server.Controllers;

using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

using static OpenIddict.Abstractions.OpenIddictConstants;

[ApiExplorerSettings(IgnoreApi = true)]
public class UserinfoController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserinfoController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("~/connect/userinfo"), HttpPost("~/connect/userinfo")]
    [IgnoreAntiforgeryToken, Produces("application/json")]
    public async Task<IActionResult> Userinfo()
    {
        var user = await this.userManager.GetUserAsync(this.User);
        if (user is null)
        {
            return Challenge(
                new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "The specified access token is bound to an account that no longer exists."
                }),
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var claims = new Dictionary<string, object>(StringComparer.Ordinal)
        {
            // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
            [Claims.Subject] = await this.userManager.GetUserIdAsync(user)
        };

        if (this.User.HasScope(Scopes.Email))
        {
            claims[Claims.Email] = await this.userManager.GetEmailAsync(user);
            claims[Claims.EmailVerified] = await this.userManager.IsEmailConfirmedAsync(user);
        }

        if (this.User.HasScope(Scopes.Phone))
        {
            claims[Claims.PhoneNumber] = await this.userManager.GetPhoneNumberAsync(user);
            claims[Claims.PhoneNumberVerified] = await this.userManager.IsPhoneNumberConfirmedAsync(user);
        }

        if (this.User.HasScope(Scopes.Roles))
        {
            claims[Claims.Role] = await this.userManager.GetRolesAsync(user);
        }

        // Note: the complete list of standard claims supported by the OpenID Connect specification
        // can be found here: http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims

        return Ok(claims);
    }
}
