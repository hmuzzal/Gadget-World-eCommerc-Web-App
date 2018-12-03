namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product13123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Title");
        }
    }
}
