namespace CentralServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AfterMerge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductNumber = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.PurchasedProducts",
                c => new
                    {
                        PurchasedProductId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Name = c.String(),
                        ProductNumber = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Purchase_PurchaseId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchasedProductId)
                .ForeignKey("dbo.Purchases", t => t.Purchase_PurchaseId)
                .Index(t => t.Purchase_PurchaseId);
            
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
            DropForeignKey("dbo.PurchasedProducts", "Purchase_PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.PurchasedProducts", new[] { "Purchase_PurchaseId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchasedProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
        }
    }
}
