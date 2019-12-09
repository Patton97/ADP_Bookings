using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    class Company
    {
        public Company() { /* */ }

        [Key]
        public int CompanyID { get; set; }
        public string Name { get; set; }

        [ForeignKey("Departments")]
        public List<Department> Departments { get; set; }
    }
}
