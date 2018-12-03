namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productIdNotNUllabe : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "CategoryId", c => c.Int());
        }
    }
}
