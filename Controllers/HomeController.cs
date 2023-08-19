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

    [HttpGet("{id}")]
    public IActionResult GetOne(int id)
    {
        Monster? single = dbContext.Monsters.FirstOrDefault(x => x.MonsterId == id);
        // Other code

        return View("GetOne", single);
    }

    [HttpPost("create")]
    public IActionResult CreateOne(Monster monster)
    {
        //dbContext.Monsters.Add(monster);

        // OR

        dbContext.Add(monster);
        dbContext.SaveChanges();

        return RedirectToAction("Index");
    }


    // Inside HomeController
    [HttpPost("update/{Id}")]
    public IActionResult UpdateOne(int Id)
    {
        // We must first Query for a single user from our Context object to track changes.
        Monster? RetrievedUser = dbContext.Monsters.FirstOrDefault(x => x.MonsterId == Id);
        // Then we may modify properties of this tracked model object
        RetrievedUser.Name = "New name";
        RetrievedUser.UpdatedAt = DateTime.Now;

        // Finally, .SaveChanges() will update the DB with these new values
        dbContext.SaveChanges();

        // Other code

        return RedirectToAction("Index");
    }

    [HttpDelete("delete/{Id}")]
    public IActionResult Delete(int id)
    {
        // Like Update, we will need to query for a single user from our Context object
        Monster? RetrievedUser = dbContext.Monsters.SingleOrDefault(x =>x.MonsterId == id);

        // Then pass the object we queried for to .Remove() on Users
        dbContext.Monsters.Remove(RetrievedUser);

        // Finally, .SaveChanges() will remove the corresponding row representing this User from DB 
        dbContext.SaveChanges();

        return RedirectToAction("Index");


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
