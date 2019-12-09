using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    class Activity
    {
        public Activity() { /* */ }

        [Key]
        public int ActivityID { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }
    }
}
