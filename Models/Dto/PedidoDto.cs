using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodCom.Models.Dto
{
    public class PedidoDto
    {
            [Required]
            public int ClienteId { get; set; }

            [Column(TypeName = "decimal(18,2)")]
            public decimal PedidoTotal { get; set; }

            public StatusPedido Status { get; set; } = StatusPedido.Pendente;

            public DateTime DataCriacao { get; set; } = DateTime.Now;

            public DateTime? DataAtualizacao { get; set; }

    }
}
