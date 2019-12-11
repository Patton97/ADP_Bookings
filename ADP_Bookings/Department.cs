using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    public class Department
    {
        public Department(int departmentID, string name)
        {
            DepartmentID = departmentID;
            Name = name;
        }
        
        //Scalar Properties
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        [ForeignKey("BookingID")]
        public virtual ICollection<Booking> Bookings { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }
    }
}
