using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public UnitOfWorkFactory() { /* */ }
        public IUnitOfWork Create() => new UnitOfWork(new ADP_DBContext());
    }
}
