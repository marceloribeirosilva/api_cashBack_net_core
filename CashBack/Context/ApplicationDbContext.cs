using CashBack.Models;
using CashBack.Models.Maps;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options){}

        public DbSet<Disco> CatalogoDiscos { get; set; }
        public DbSet<CashBackPercentual> CashBackPercentuais { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVendas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disco>().HasKey(p => p.ID);
            modelBuilder.Entity<CashBackPercentual>().HasKey(p => p.ID);

            modelBuilder.ApplyConfiguration(new VendaMap());
            modelBuilder.ApplyConfiguration(new ItemVendaMap());
        }
    }
}
