namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productupadted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Catagory_CatagoryId", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "Catagory_CatagoryId" });
            AddColumn("dbo.Products", "CategoryName", c => c.String());
            AddColumn("dbo.Products", "ImageData", c => c.Binary());
            DropColumn("dbo.Products", "CataCatagoryId");
            DropColumn("dbo.Products", "ImageLink");
            DropColumn("dbo.Products", "Catagory_CatagoryId");
            DropTable("dbo.Catagories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        CatagoryId = c.Int(nullable: false, identity: true),
                        CatagoryType = c.String(),
                    })
                .PrimaryKey(t => t.CatagoryId);
            
            AddColumn("dbo.Products", "Catagory_CatagoryId", c => c.Int());
            AddColumn("dbo.Products", "ImageLink", c => c.String());
            AddColumn("dbo.Products", "CataCatagoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ImageData");
            DropColumn("dbo.Products", "CategoryName");
            CreateIndex("dbo.Products", "Catagory_CatagoryId");
            AddForeignKey("dbo.Products", "Catagory_CatagoryId", "dbo.Catagories", "CatagoryId");
        }
    }
}
