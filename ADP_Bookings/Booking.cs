using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    public class Booking
    {
        public Booking()
        {
            Activities = new List<Activity>();
        }

        //Scalar Properties
        [Key]
        public int BookingID { get; set; }
        public DateTime Date { get; set; }
        public float EstimatedCost { get; set; }
        public float ActualCost { get; set; }
        public int NumAttendees { get; set; }

        //Navigation Properties
        [ForeignKey("ActivityID")]
        public virtual ICollection<Activity> Activities { get; set; }
        [ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; }

        [ForeignKey("Department")]        
        public virtual Department Department { get; set; }
    }
}
