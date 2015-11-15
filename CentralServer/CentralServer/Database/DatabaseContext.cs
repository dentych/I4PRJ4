using System.Data.Entity;
using SharedLib.Models;

namespace ServerDatabase
{
    class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("YourConnectionName") { }

        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}
