namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class undochanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Title", c => c.String());
            DropTable("dbo.Colors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ColorID = c.Int(nullable: false, identity: true),
                        ColorType = c.String(),
                    })
                .PrimaryKey(t => t.ColorID);
            
            DropColumn("dbo.Products", "Title");
        }
    }
}
