namespace CapstoneWIE.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBlogState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPost", "BlogState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPost", "BlogState");
        }
    }
}
