using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings.Models
{
    public class Department : IRecord
    {
        public Department()
        {
            Bookings = new List<Booking>();
        }
        public Department(int departmentID, string name, Company company) :this()
        {
            DepartmentID = departmentID;
            Name = name;
            Company = company;
        }
        
        //Scalar Properties
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        public string Name { get; set; }

        //Navigation Properties
        [Required]
        public Company Company { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        
    }
}
