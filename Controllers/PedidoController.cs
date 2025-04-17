using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFoodCom.Data;
using ProjetoFoodCom.Models;
using ProjetoFoodCom.Models.Dto;

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
        
        return View("Index", item);
    }
    [Authorize]
    [HttpPost]
    [Route("/Pedido/NovoPedido/{itemId}")]
    public IActionResult ConfirmarPedido(int itemId,int quantidade)
    {
        var item = _context.Itens
            .FirstOrDefault(i => i.Id == itemId);

        if (item == null)
        {
            return NotFound();
        }

        var pedido = new Pedido
        {
            ClienteId = int.Parse(User.FindFirst("ClienteId")?.Value),
            PedidoTotal = item.Preco * quantidade,
            Status = StatusPedido.Pendente,
            DataCriacao = DateTime.Now
        };

        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var itemPedido = new ItemPedido
        {
            PedidoId = pedido.PedidoId,
            ItemId = item.Id,
            Quantidade = quantidade,
            PrecoUnitario = item.Preco,
            Subtotal = item.Preco * quantidade
        };

        _context.ItensPedido.Add(itemPedido);
        _context.SaveChanges();

        return RedirectToAction("Index","Home");

    }

}