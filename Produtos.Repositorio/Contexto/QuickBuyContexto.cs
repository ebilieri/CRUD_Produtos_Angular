using GroceryShop.Dominio.Entidades;
using GroceryShop.Repositorio.Config;
using Microsoft.EntityFrameworkCore;

namespace GroceryShop.Repositorio.Contexto
{
    public class QuickBuyContexto : DbContext
    {

        public DbSet<Produto> Produtos { get; set; }


        public QuickBuyContexto(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento das entidades
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
