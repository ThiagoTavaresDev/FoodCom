using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodCom.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [Phone]
        [StringLength(20)]
        public string Telefone { get; set; }
        
        [StringLength(200)]
        public string Endereco { get; set; }
        
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
