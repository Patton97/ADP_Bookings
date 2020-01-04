﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ADP_Bookings.Models;

namespace ADP_Bookings
{
    public class Activity : IRecord
    {
        public Activity() { /* */ }

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
    }
}
