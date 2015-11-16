using System;
using System.Linq;
using System.Data.Entity;
using SharedLib.Models;

namespace CentralServer.Database
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("LocalDBv11") { }

        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        public Product CreateProduct(string name, string productNumber, decimal price, int categoryId)
        {
            var product = new Product()
            {
                Name = name,
                ProductNumber = productNumber,
                Price = price,
                ProductCategoryId = categoryId,
            };

            Products.Add(product);

            return product;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.ProductCategory)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }   
    }
}
