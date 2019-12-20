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

            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                /*
                Company tesco = new Company(1, "Tesco");
                Department hr = new Department(1, "HR", tesco);
                Department legal = new Department(2, "Legal", tesco);
                tesco.Departments.Add(hr);
                tesco.Departments.Add(legal);
                unitOfWork.Companies.Add(tesco);
                */
                unitOfWork.SaveChanges();
            }
            Application.Run(new frm_companies());
        }
    }
}
