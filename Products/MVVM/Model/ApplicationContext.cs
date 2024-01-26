using Microsoft.EntityFrameworkCore;

namespace Products.MVVM.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<ProductsDB> ProductsDBs { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=productsDB.db");
        }
    }
}
