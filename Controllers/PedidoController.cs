using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFoodCom.Data;

namespace ProjetoFoodCom.Controllers;

public class PedidoController : Controller
{
    private readonly AppDbContext _context;

    public PedidoController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [HttpGet]
    [Route("/Pedido/{id}")]

    public IActionResult GetById(int id)
    {
        var pedido =  _context.Pedidos.Find(id);
        
        return View("Index",pedido);
    }
}