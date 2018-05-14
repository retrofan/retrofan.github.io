namespace CapstoneWIE.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyContentLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogPost", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Page", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Page", "Content", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.BlogPost", "Content", c => c.String(nullable: false, maxLength: 1024));
        }
    }
}
