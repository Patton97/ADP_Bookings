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

            CreateDB();
            /*
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                // Example1
                var booking1 = unitOfWork.Bookings.Get(1);

                // Example2
                //var bookings = unitOfWork.Bookings.GetBookingsFromDepartment(1);

                // Example3
                var departments = unitOfWork.Authors.GetAuthorWithCourses(1);
                unitOfWork.Courses.RemoveRange(author.Courses);
                unitOfWork.Authors.Remove(author);
                unitOfWork.Complete();
            }*/


            //Application.Run(new frm_CreateBooking());
        }

        static void CreateDB()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Departments.Add(new Department(1, "HR"));

                unitOfWork.Complete();
            }
        }

        static void CreateDB2()
        {
            using (var context = new ADP_DBContext())
            {

                var companyList = context.Companies.ToList();
                Company company = new Company(1, "Tesco");
                Department hr = new Department(1, "HR");
                Department legal = new Department(2, "Legal");
                company.Departments.Add(hr);
                company.Departments.Add(legal);



                context.SaveChanges();
            }
        }
    }
}
