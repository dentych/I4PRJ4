using System;
using System.Collections.Generic;
using System.Linq;
using SharedLib.Models;
using System.Data.Entity;

namespace ServerDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                Console.Write("Enter product names: ");

                // Create Purchase
                var purchase = new Purchase();
                purchase.PurchasedProducts = new List<PurchasedProduct>();

                

                // Make 5 PurchasedProducts and add them to Purchase
                for (int i = 0; i < 5; i++)
                {
                var name = Console.ReadLine();
                var pp = new PurchasedProduct { Name = name };
                db.PurchasedProducts.Add(pp);
                purchase.PurchasedProducts.Add(pp);
                }

                //var number = Console.ReadLine();
                //var price = Console.ReadLine();
                //var product = new Product { Name = name };
                //db.Products.Add(product);

                // Add to database
                db.Purchases.Add(purchase);

                // Save and implement
                db.SaveChanges();

                Console.WriteLine();
                Console.WriteLine();

                // Display Products
                var query = from p in db.Products
                            orderby p.Name
                            select p;
                Console.WriteLine("All Products");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine();
                Console.WriteLine();

                // Display PurchasedProducts
                var query2 = from p in db.PurchasedProducts
                            orderby p.Name
                            select p;
                Console.WriteLine("All PurchasedProducts");
                foreach (var item in query2)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();


            }
        }
    }

    public class BloggingContext : DbContext
    {
        public BloggingContext()
        {
            // Makes Migration Redundant... Magics...
            //System.Data.Entity.Database.SetInitializer<BloggingContext>(null);
        }
        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

    }



}

