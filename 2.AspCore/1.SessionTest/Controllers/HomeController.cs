using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _1.SessionTest.Models;
using _1.SessionTest.Helpers;

namespace _1.SessionTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    const string SessionKeyName = "_Name";
    const string SessionKeyFY = "_FY";
    const string SessionKeyDate = "_Date";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        HttpContext.Session.SetString(SessionKeyName, "Park Was");
        HttpContext.Session.SetInt32(SessionKeyFY, 2022);

        HttpContext.Session.Set(SessionKeyDate, DateTime.Now);

        HttpContext.Session.SetInt32("age", 20);

        HttpContext.Session.SetString("username", "abc");

        Product product = new Product
        {
            Id = "p01",
            Name = "Name 1",
            Price = 5
        };
        HttpContext.Session.Set<Product>("product", product);

        List<Product> products = new List<Product>() {
                new Product {
                    Id = "p01",
                    Name = "Name 1",
                    Price = 5
                },
                new Product {
                    Id = "p02",
                    Name = "Name 2",
                    Price = 9
                },
                new Product {
                    Id = "p03",
                    Name = "Name 3",
                    Price = 2
                }
            };
        HttpContext.Session.Set<List<Product>>("products", products);
        return View();
    }

    public IActionResult Privacy()
    {
        ViewBag.Name = HttpContext.Session.GetString(SessionKeyName);
        ViewBag.FY = HttpContext.Session.GetInt32(SessionKeyFY);
        ViewBag.Date = HttpContext.Session.Get<DateTime>(SessionKeyDate);
        ViewData["Message"] = "Session State In Asp.Net Core 6.0";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
