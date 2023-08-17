using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNet_EFCore.Models;
using System;

namespace DotNet_EFCore.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    // DbContext injection
    private MyContext dbContext;

    // here we can "inject" our context service into the constructor
    public HomeController(MyContext context, ILogger<HomeController> logger)
    {
        dbContext = context;
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Monster> AllMonsters = dbContext.Monsters.ToList();

        return View();
    }


    [HttpGet("")]
    public IActionResult Example()
    {
        // Get all Users
        ViewBag.AllUsers = dbContext.Monsters.ToList();

        // Get Users with the LastName "Jefferson"
        ViewBag.Jeffersons = dbContext.Monsters
            .Where(u => u.Name == "Jason")
            .ToList();

        // Get the 5 most recently added Users
        ViewBag.MostRecent = dbContext.Monsters
            .OrderByDescending(u => u.CreatedAt)
            .Take(5)
            .ToList();

        return View();
    }

    [HttpGet("/{id}")]
    public IActionResult GetOne(int id)
    {
        Monster? single = dbContext.Monsters.FirstOrDefault(x => x.MonsterId == id);
        // Other code

        return View("GetOne", single);
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
