using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    class Department
    {
        public Department() { /* */ }

        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public float EstimatedCost { get; set; }

        [ForeignKey("Bookings")]
        public List<Booking> Bookings;
        [ForeignKey("CompanyID")]
        public int CompanyID;
    }
}
