using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Route("/Pedido/NovoPedido/{itemId}")]
    public IActionResult NovoPedido(int itemId)
    {
        var item = _context.Itens
            .FirstOrDefault(i => i.Id == itemId);
        
        if (item == null)
        {
            return NotFound();
        }
        
        // Você pode criar um ViewModel específico para esta tela
        // ou passar o item diretamente para a view
        return View("Index", item);
    }


}