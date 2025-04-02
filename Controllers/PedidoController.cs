using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoFoodCom.Controllers;

public class PedidoController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}