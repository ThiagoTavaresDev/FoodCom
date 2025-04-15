using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFoodCom.Models
{
    public class ItemPedido
    {
        public int ItemPedidoId { get; set; }
        
        public int PedidoId { get; set; }
        
        [ForeignKey("PedidoId")]
        public virtual Pedido Pedido { get; set; }
        
        public int ItemId { get; set; }
        
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1")]
        public int Quantidade { get; set; } = 1;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal 
        { 
        get => Quantidade * PrecoUnitario; 
        set { } // Empty setter for EF Core
        }
}
}
