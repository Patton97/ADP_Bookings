using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings
{
    public class Company
    {
        public Company(int companyID, string name)
        {
            CompanyID = companyID;
            Name = name;
        }
        
        //Scalar Properties
        [Key]
        public int CompanyID { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        [ForeignKey("Departments")]
        public virtual List<Department> Departments { get; set; }


    }
}
