namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colormodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ColorID = c.Int(nullable: false, identity: true),
                        ColorType = c.String(),
                    })
                .PrimaryKey(t => t.ColorID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Colors");
        }
    }
}
