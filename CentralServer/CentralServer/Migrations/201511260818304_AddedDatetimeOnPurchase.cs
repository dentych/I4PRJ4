namespace CentralServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDatetimeOnPurchase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "DateCreated");
        }
    }
}
