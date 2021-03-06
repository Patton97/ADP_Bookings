﻿//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADP_Bookings.Models
{
    public class Booking : IRecord
    {
        public Booking()
        {
            Activities = new List<Activity>();
        }
        public Booking(int bookingID, string name, DateTime date, float cost, Department department) : this()
        {
            BookingID = bookingID;
            Name = name;
            Date = date;
            EstimatedCost = cost;
            Department = department;
        }

        //Scalar Properties
        [Key]
        public int BookingID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public float EstimatedCost { get; set; }
        public float ActualCost { get; set; }
        public int NumAttendees { get; set; }

        //Navigation Properties
        public virtual ICollection<Activity> Activities { get; set; }
        [Required]
        public virtual Department Department { get; set; }
    }
}
