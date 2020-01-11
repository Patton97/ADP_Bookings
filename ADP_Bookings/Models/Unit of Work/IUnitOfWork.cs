using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        IDepartmentRepository Departments { get; }
        IBookingRepository Bookings { get; }
        IActivityRepository Activities { get; }
        int SaveChanges();
    }
}
