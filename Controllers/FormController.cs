using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMoviw.Models;

namespace MvcMoviw.Controllers;

public class FormController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public FormController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Form formData)
    {
        if(formData.UserName == "admin" && formData.Password == "admin")
        {
            Response.Cookies.Append("UserName", formData.UserName);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewData["Message"] = "Invalid username or password";
            return View();
        }
        //Validate formData
        // _logger.LogInformation("Form data: " + formData.FirstName + " " + formData.LastName + " " + formData.Email);
        // Console.WriteLine(formData.FirstName);
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
