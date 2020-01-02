namespace ADP_Bookings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "Department_DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Company_CompanyID", "dbo.Companies");
            DropIndex("dbo.Bookings", new[] { "Department_DepartmentID" });
            DropIndex("dbo.Departments", new[] { "Company_CompanyID" });
            AddColumn("dbo.Bookings", "Name", c => c.String());
            AlterColumn("dbo.Bookings", "Department_DepartmentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "Company_CompanyID", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "Department_DepartmentID");
            CreateIndex("dbo.Departments", "Company_CompanyID");
            AddForeignKey("dbo.Bookings", "Department_DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "Company_CompanyID", "dbo.Companies", "CompanyID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Company_CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Bookings", "Department_DepartmentID", "dbo.Departments");
            DropIndex("dbo.Departments", new[] { "Company_CompanyID" });
            DropIndex("dbo.Bookings", new[] { "Department_DepartmentID" });
            AlterColumn("dbo.Departments", "Company_CompanyID", c => c.Int());
            AlterColumn("dbo.Departments", "Name", c => c.String());
            AlterColumn("dbo.Bookings", "Department_DepartmentID", c => c.Int());
            DropColumn("dbo.Bookings", "Name");
            CreateIndex("dbo.Departments", "Company_CompanyID");
            CreateIndex("dbo.Bookings", "Department_DepartmentID");
            AddForeignKey("dbo.Departments", "Company_CompanyID", "dbo.Companies", "CompanyID");
            AddForeignKey("dbo.Bookings", "Department_DepartmentID", "dbo.Departments", "DepartmentID");
        }
    }
}
