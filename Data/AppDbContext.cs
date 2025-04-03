using Microsoft.EntityFrameworkCore;
using ProjetoFoodCom.Models;

namespace ProjetoFoodCom.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de relacionamentos
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.ItensPedido)
                .WithOne(ip => ip.Pedido)
                .HasForeignKey(ip => ip.PedidoId);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.ItensPedido)
                .WithOne(ip => ip.Item)
                .HasForeignKey(ip => ip.ItemId);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);

            // Configurações de tipos decimais
            modelBuilder.Entity<Pedido>()
                .Property(p => p.PedidoTotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Item>()
                .Property(i => i.Preco)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ItemPedido>()
                .Property(ip => ip.PrecoUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ItemPedido>()
                .Property(ip => ip.Subtotal)
                .HasColumnType("decimal(18,2)");
        }
}