namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _015 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Type_Id", "dbo.UserTypes");
            DropIndex("dbo.Users", new[] { "Type_Id" });
            AddColumn("dbo.Users", "Type", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Type_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Type_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Type");
            CreateIndex("dbo.Users", "Type_Id");
            AddForeignKey("dbo.Users", "Type_Id", "dbo.UserTypes", "Id", cascadeDelete: true);
        }
    }
}
