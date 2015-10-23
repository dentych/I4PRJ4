namespace CentralServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductNumber = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.PurchasedProducts",
                c => new
                    {
                        PurchasedProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductNumber = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PurchasedProductId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PurchaseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchasedProducts");
            DropTable("dbo.Products");
        }
    }
}
