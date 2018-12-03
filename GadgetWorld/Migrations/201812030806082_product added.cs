namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.Int());
            DropColumn("dbo.Products", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryName", c => c.String());
            DropColumn("dbo.Products", "CategoryId");
        }
    }
}
