using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    public class Activity
    {
        public Activity() { /* */ }

        //Scalar Properties
        [Key]
        public int ActivityID { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Notes { get; set; }
    }
}
