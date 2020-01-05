namespace ADP_Bookings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingActivities",
                c => new
                    {
                        Booking_BookingID = c.Int(nullable: false),
                        Activity_ActivityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Booking_BookingID, t.Activity_ActivityID })
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingID, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.Activity_ActivityID, cascadeDelete: true)
                .Index(t => t.Booking_BookingID)
                .Index(t => t.Activity_ActivityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingActivities", "Activity_ActivityID", "dbo.Activities");
            DropForeignKey("dbo.BookingActivities", "Booking_BookingID", "dbo.Bookings");
            DropIndex("dbo.BookingActivities", new[] { "Activity_ActivityID" });
            DropIndex("dbo.BookingActivities", new[] { "Booking_BookingID" });
            DropTable("dbo.BookingActivities");
        }
    }
}
