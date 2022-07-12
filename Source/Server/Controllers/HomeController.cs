using Microsoft.AspNetCore.Mvc;

namespace LegendaryTelegram.Server.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";
        return View();
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";
        return View();
    }

    public IActionResult Error() => View("~/Views/Shared/Error.cshtml");
}

