//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using ADP_Bookings.Forms;
using ADP_Bookings.Models;

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
            //AppDomain.SetData()

            Application.Run(new frm_companies());
        }
    }
}
