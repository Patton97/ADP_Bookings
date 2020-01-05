namespace ADP_Bookings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Booking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "Booking_BookingID", "dbo.Bookings");
            DropIndex("dbo.Activities", new[] { "Booking_BookingID" });
            DropColumn("dbo.Activities", "Booking_BookingID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activities", "Booking_BookingID", c => c.Int());
            CreateIndex("dbo.Activities", "Booking_BookingID");
            AddForeignKey("dbo.Activities", "Booking_BookingID", "dbo.Bookings", "BookingID");
        }
    }
}
