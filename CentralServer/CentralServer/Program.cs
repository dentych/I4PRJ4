using CentralServer.Database;
using CentralServer.Handlers;
using CentralServer.Logging;
using CentralServer.Logging.Loggers;
using CentralServer.Messaging;
using CentralServer.Server;
using CentralServer.Sessions;
using CentralServer.Threading;
using SharedLib.Models;
using System;
using System.Linq;

namespace CentralServer
{
    class CentralServerMain
    {
        static void Main(string[] args)
        {


            var log = new Log(new ConsoleColorLogger(), Log.DEBUG);

            LogCheck(log);
            log.Write("Main", Log.NOTICE, "Initiating");

            // Init MainControl
            var sessions = new SessionControl();
            var handler = new MainCommandHandler(log, sessions);
            var mainControl = new MainControl(log, sessions, handler);
            var mainRunner = new MessageReceiver(mainControl, new MessageQueue());

            // Init socket server
            var port = GetServerPort(args);
            var server = new SocketServer(log, mainRunner, port);

            // Start all threads
            var mainThread = ThreadStarter.Start(mainRunner);
            var serverThread = ThreadStarter.Start(server);

            // Join on all threads
            mainThread.Join();
            serverThread.Join();
        }

        private static int GetServerPort(string[] args)
        {
            return 11000;
        }

        private static void LogCheck(ILog log)
        {
            log.Write("LogCheck", Log.ERROR, "Category: ERROR");
            log.Write("LogCheck", Log.WARNING, "Category: WARNING");
            log.Write("LogCheck", Log.NOTICE, "Category: NOTICE");
            log.Write("LogCheck", Log.DEBUG, "Category: DEBUG");
        }

        private static void TestHandler()
        {
            /*
            using(var ctx = new DatabaseContext())
            {
                var product = new Product()
                {
                    Name = "HAHA",
                    ProductNumber = "1234",
                    Price = 123,
                    ProductCategoryId = 1,
                };

                ctx.Products.Add(product);
                ctx.SaveChanges();
            }



            using (var ctx = new DatabaseContext())
            {
                var product = ctx.Products.Find(1002);

                if (product != null)
                {
                    product.Name = "ngga";
                    product.ProductNumber = "2222";
                    product.Price = 1;
                    product.ProductCategoryId = 2;

                    ctx.SaveChanges();
                }
            }



            using (var ctx = new DatabaseContext())
            {
                var product = ctx.Products.Find(1002);

                if (product != null)
                {
                    ctx.Products.Remove(product);
                    ctx.SaveChanges();
                }
            }



            var cat = new ProductCategory() { Name = "JAKOB HE" };

            // Write to database
            using (var db = new DatabaseContext())
            {
                db.ProductCategories.Add(cat);
                db.SaveChanges();
            }


            using (var db = new DatabaseContext())
            {
                var cat = db.ProductCategories.Find(1002);

                if (cat != null)
                {
                    cat.Name = "POUL JOHN";
                    db.SaveChanges();
                }
            }


            using (var db = new DatabaseContext())
            {
                var cat = db.ProductCategories.Find(1002);

                if (cat != null)
                {
                    db.ProductCategories.Remove(cat);
                    db.SaveChanges();
                }
            }


            using (var ctx = new DatabaseContext())
            {
                var purchase = new Purchase();

                purchase.PurchasedProducts.Add(new PurchasedProduct()
                {
                    Name = "Testvare 1",
                    ProductNumber = "1234",
                    Quantity = 2,
                    UnitPrice = 11m,
                });

                purchase.PurchasedProducts.Add(new PurchasedProduct()
                {
                    Name = "Testvare 2",
                    ProductNumber = "1222234",
                    Quantity = 1,
                    UnitPrice = 12345m,
                });

                ctx.Purchases.Add(purchase);
                ctx.SaveChanges();
            }
            */
        }
    }
}
