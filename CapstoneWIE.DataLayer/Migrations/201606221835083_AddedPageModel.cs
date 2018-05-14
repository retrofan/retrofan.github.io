namespace CapstoneWIE.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPageModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogPost", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BlogPost", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.Page",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false, maxLength: 1024),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
            AlterColumn("dbo.BlogPost", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BlogPost", "ApplicationUserId");
            AddForeignKey("dbo.BlogPost", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPost", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Page", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Page", new[] { "ApplicationUserId" });
            DropIndex("dbo.BlogPost", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BlogPost", "ApplicationUserId", c => c.String(maxLength: 128));
            DropTable("dbo.Page");
            CreateIndex("dbo.BlogPost", "ApplicationUserId");
            AddForeignKey("dbo.BlogPost", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
