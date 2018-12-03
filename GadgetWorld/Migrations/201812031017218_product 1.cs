namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImagePath", c => c.String());
            DropColumn("dbo.Products", "ImageData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageData", c => c.Binary());
            DropColumn("dbo.Products", "ImagePath");
        }
    }
}
