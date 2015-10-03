namespace ServerDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFKtoPurchasedProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchasedProducts", "PurchaseId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchasedProducts", "PurchaseId");
            AddForeignKey("dbo.PurchasedProducts", "PurchaseId", "dbo.Purchases", "PurchaseId", cascadeDelete: true);
            DropColumn("dbo.Purchases", "PurchasedProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "PurchasedProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchasedProducts", "PurchaseId", "dbo.Purchases");
            DropIndex("dbo.PurchasedProducts", new[] { "PurchaseId" });
            DropColumn("dbo.PurchasedProducts", "PurchaseId");
        }
    }
}
