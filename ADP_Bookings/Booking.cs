using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    class Booking
    {
        public Booking()
        {
            Activities = new List<Activity>();
        }

        [Key]
        public int BookingID { get; set; }
        public DateTime Date { get; set; }
        public float EstimatedCost { get; set; }
        public float ActualCost { get; set; }
        public int NumAttendees { get; set; }

        [ForeignKey("Activities")]
        public List<Activity> Activities;
        [ForeignKey("DepartmentID")]
        public int DepartmentID;
    }
}
