using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNet_EFCore.Models;

namespace DotNet_EFCore.Controllers;

public class HomeController : Controller
{


    private MyContext dbContext;

    // here we can "inject" our context service into the constructor
    public HomeController(MyContext context)
    {
        dbContext = context;
    }

    public IActionResult Index()
    {
        return View();
    }






    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
