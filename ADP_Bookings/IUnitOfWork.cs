using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        void SaveChanges();
    }
}
