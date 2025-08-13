using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Verifica si hay sesión activa
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
        {
            return RedirectToAction("Login", "Account");
        }

        return View(); // Esta sí usa el layout
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
