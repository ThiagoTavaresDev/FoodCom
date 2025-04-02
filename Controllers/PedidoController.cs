using Microsoft.AspNetCore.Mvc;

namespace ProjetoFoodCom.Controllers;

public class PedidoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}