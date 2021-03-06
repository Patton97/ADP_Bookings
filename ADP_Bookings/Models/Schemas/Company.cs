﻿//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings.Models
{
    public class Company : IRecord
    {
        public Company()
        {
            Departments = new List<Department>();
        }

        public Company(int companyID, string name) : this()
        {
            CompanyID = companyID;
            Name = name;
        }
        
        //Scalar Properties
        [Key]
        public int CompanyID { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public virtual ICollection<Department> Departments { get; set; }
    }
}
