using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    //Primarily facilitates mocking the context during unit tests
    public interface IADP_DBContext 
    {
        DbSet<Company> Companies { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Activity> Activities { get; set; }
    }
}
