//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings.Models
{
    public class Activity : IRecord
    {
        public Activity() { bookings = new List<Booking>(); }

        public Activity(int activityID, string name, float cost, string notes) : this()
        {
            ActivityID = activityID;
            Name = name;
            Cost = cost;
            Notes = notes;
        }

        //Scalar Properties
        [Key]
        public int ActivityID { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Notes { get; set; }

        //Navigation Properties
        public virtual ICollection<Booking> bookings { get; set; }
    }
}
