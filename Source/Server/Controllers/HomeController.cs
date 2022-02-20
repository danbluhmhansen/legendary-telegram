namespace BlazorApp1.Server.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult About()
    {
        this.ViewData["Message"] = "Your application description page.";

        return View();
    }

    public IActionResult Contact()
    {
        this.ViewData["Message"] = "Your contact page.";

        return View();
    }

    public IActionResult Error() => View("~/Views/Shared/Error.cshtml");
}
