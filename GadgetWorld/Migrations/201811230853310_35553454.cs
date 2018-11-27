namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _35553454 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CataCatagoryId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                        ImageLink = c.String(),
                        Catagory_CatagoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Catagories", t => t.Catagory_CatagoryId)
                .Index(t => t.Catagory_CatagoryId);
            
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        CatagoryId = c.Int(nullable: false, identity: true),
                        CatagoryType = c.String(),
                    })
                .PrimaryKey(t => t.CatagoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Catagory_CatagoryId", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "Catagory_CatagoryId" });
            DropTable("dbo.Catagories");
            DropTable("dbo.Products");
        }
    }
}
