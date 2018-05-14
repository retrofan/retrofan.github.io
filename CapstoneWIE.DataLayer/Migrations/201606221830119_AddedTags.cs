namespace CapstoneWIE.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagBlogPost",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        BlogPost_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.BlogPost_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.BlogPost", t => t.BlogPost_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.BlogPost_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagBlogPost", "BlogPost_Id", "dbo.BlogPost");
            DropForeignKey("dbo.TagBlogPost", "Tag_Id", "dbo.Tag");
            DropIndex("dbo.TagBlogPost", new[] { "BlogPost_Id" });
            DropIndex("dbo.TagBlogPost", new[] { "Tag_Id" });
            DropTable("dbo.TagBlogPost");
            DropTable("dbo.Tag");
        }
    }
}
