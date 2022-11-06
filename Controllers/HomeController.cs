using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMoviw.Models;

namespace MvcMoviw.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        
        // ViewData["UserName"]=Request.Cookies["UserName"];

        ViewData["UserName"] = HttpContext.Session.GetString("name");
        ViewData["message"] = TempData["mytemp"];
        ViewData["name"] = HttpContext.Request.Query["name"];


        return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Hunxa()
    {
        // ViewData["Message"] = "Your application description page.";
        return View();

        // Redirects to /Home/Privacy
        // return RedirectToAction("Privacy", "Home");  
        // var countries = new string[] { "India", "USA", "UK", "Canada", "Australia" };

        // return Json(countries);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
