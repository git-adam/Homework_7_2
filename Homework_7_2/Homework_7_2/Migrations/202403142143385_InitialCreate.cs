namespace Homework_7_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Number = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        DismissDate = c.DateTime(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bonus = c.Boolean(nullable: false),
                        Comments = c.String(),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Statuses", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "StatusId", "dbo.Statuses");
            DropIndex("dbo.Employees", new[] { "StatusId" });
            DropTable("dbo.Statuses");
            DropTable("dbo.Employees");
        }
    }
}
