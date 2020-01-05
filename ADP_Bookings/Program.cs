using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using ADP_Bookings.Forms;

namespace ADP_Bookings
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //ForceDB();

            Application.Run(new frm_companies());
        }

        
        static void ForceDB()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                Activity a = new Activity(0, "CLI_TEST", 36, "NotesTest");
                unitOfWork.Activities.Add(a);

                Company c = new Company(0, "CLI_TEST");
                unitOfWork.Companies.Add(c);

                Department d = new Department(0, "CLI_TEST", c);
                unitOfWork.Departments.Add(d);               

                Booking b = new Booking(0, "CLI_TEST", DateTime.Today, 67, d);
                b.Activities.Add(a);
                unitOfWork.Bookings.Add(b);

                Console.WriteLine("Local: " + b.Activities.Count);
                Console.WriteLine("DB:    " + unitOfWork.Bookings.Get(b.BookingID).Activities.Count);

                unitOfWork.SaveChanges();
            }
        }
    }
}
