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
        public virtual ICollection<Activity> Activities { get; set; }
        [Required]
        public virtual Department Department { get; set; }
    }
}
