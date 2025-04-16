using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProjetoFoodCom.Models
{
    public enum StatusPedido
    {
        Pendente,
        EmPreparo,
        EmEntrega,
        Entregue,
        Cancelado
    }

    public class Pedido
    {
        public int PedidoId { get; set; }
        
        [Required]
        public int ClienteId { get; set; }
        
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PedidoTotal { get; set; }
        
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
        
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        public DateTime? DataAtualizacao { get; set; }
        
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
