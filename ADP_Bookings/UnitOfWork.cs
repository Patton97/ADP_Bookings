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
            Bookings = new BookingRepository(ctx);
        }

        public IBookingRepository Bookings { get; private set; }

        public void SaveChanges() => ctx.SaveChanges();
        public void Dispose() => ctx.Dispose();
    }
}
