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

        var pedidoDto = new PedidoDto
        {
            ClienteId = 1,
            PedidoTotal = item.Preco * quantidade,
            Status = StatusPedido.Pendente,
            DataCriacao = DateTime.Now
        };

        // Converter DTO para entidade
        var pedido = new Pedido
        {
            ClienteId = pedidoDto.ClienteId,
            PedidoTotal = pedidoDto.PedidoTotal,
            Status = pedidoDto.Status,
            DataCriacao = pedidoDto.DataCriacao
        };

        if (item == null)
        {
            return NotFound();
        }

        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var itemPedido = new ItemPedido
        {
            PedidoId = 2,
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