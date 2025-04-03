using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoFoodCom.Models;

namespace ProjetoFoodCom.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}