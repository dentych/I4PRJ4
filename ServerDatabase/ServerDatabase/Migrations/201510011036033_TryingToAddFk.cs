namespace ServerDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingToAddFk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "PurchasedProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "PurchasedProductId");
        }
    }
}
