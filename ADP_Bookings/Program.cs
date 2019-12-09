using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Application.Run(new Form1());
        }

        static void CreateDB()
        {
            using (var context = new MyDBEntities())
            {
                var studentList = context.Students.ToList();
                Address a = new Address();
                a.AddressID = 1;
                a.NumberOrName = "20";
                a.PostCode = "LL00 GP4";
                a.Street = "BackOfBehond";
                a.Town = "Trumpton";

                context.Addresses.Add(a);
                context.SaveChanges();
            }
        }
    }
}
