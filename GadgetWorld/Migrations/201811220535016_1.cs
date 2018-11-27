namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(nullable: false),
                        ContactNumber = c.String(),
                        Address = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        Password = c.String(nullable: false, maxLength: 50),
                        RepeatPassword = c.String(),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Type_Id", "dbo.UserTypes");
            DropIndex("dbo.Users", new[] { "Type_Id" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
        }
    }
}
