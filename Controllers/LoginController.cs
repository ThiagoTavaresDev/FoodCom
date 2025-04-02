using Microsoft.AspNetCore.Mvc;

namespace ProjetoFoodCom.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
}