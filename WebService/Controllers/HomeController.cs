using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.WebService.Models;

namespace Blog.WebService.Controllers;

public class HomeController : Controller
{
    readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger)
    {
        this.logger = logger;
    }

    public IActionResult Index()
    {
        logger.LogInformation("GET The Index page is requested");
        return RedirectToAction("Articles", "Article");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

