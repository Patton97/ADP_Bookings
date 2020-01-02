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
                Company asda = unitOfWork.Companies.Get(6);
                Department dpt = new Department(0, "HR", asda);
                unitOfWork.Departments.Add(dpt);
                unitOfWork.SaveChanges();
            }
        }
    }
}
