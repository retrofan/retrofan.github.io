namespace CapstoneWIE.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyForeignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BlogPost", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.BlogPost", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BlogPost", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.BlogPost", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
