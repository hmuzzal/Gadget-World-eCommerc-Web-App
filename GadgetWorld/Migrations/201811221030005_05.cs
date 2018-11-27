namespace GadgetWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "RepeatPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RepeatPassword", c => c.String());
        }
    }
}
