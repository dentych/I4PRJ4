namespace CentralServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductCategory : DbMigration
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
            
            AddColumn("dbo.Products", "ProductCategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductCategoryId");
            DropTable("dbo.ProductCategories");
        }
    }
}
