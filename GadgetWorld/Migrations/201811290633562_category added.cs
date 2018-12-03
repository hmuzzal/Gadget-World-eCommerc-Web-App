namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CatagoryId = c.Int(nullable: false, identity: true),
                        CatagoryType = c.String(),
                    })
                .PrimaryKey(t => t.CatagoryId);
            
            AddColumn("dbo.Products", "Category_CatagoryId", c => c.Int());
            CreateIndex("dbo.Products", "Category_CatagoryId");
            AddForeignKey("dbo.Products", "Category_CatagoryId", "dbo.Categories", "CatagoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Category_CatagoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_CatagoryId" });
            DropColumn("dbo.Products", "Category_CatagoryId");
            DropTable("dbo.Categories");
        }
    }
}
