//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings.Models
{
    class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(ADP_DBContext context) : base(context) { /* */ }

        //No special behaviour (yet)

        public ADP_DBContext ADP_DBContext => Context as ADP_DBContext;
    }
}
