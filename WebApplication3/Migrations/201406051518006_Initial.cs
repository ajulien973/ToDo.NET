namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Datetime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Category = c.String(),
                        Importance = c.Int(nullable: false),
                        Urgency = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.TaskModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Datetime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Category = c.String(),
                        Importance = c.Int(nullable: false),
                        Urgency = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Tasks");
        }
    }
}
