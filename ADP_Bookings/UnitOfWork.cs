using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ADP_DBContext ctx;

        public UnitOfWork(ADP_DBContext context)
        {
            ctx = context;
            Companies = new CompanyRepository(ctx);
            Departments = new DepartmentRepository(ctx);
            Bookings = new BookingRepository(ctx);
            Activities = new ActivityRepository(ctx);
        }

        public ICompanyRepository Companies { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public IActivityRepository Activities { get; private set; }

        public int SaveChanges() => ctx.SaveChanges();
        public void Dispose() => ctx.Dispose();
    }
}
