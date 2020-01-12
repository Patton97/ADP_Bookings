using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    //This interface acts purely to support the presenters' polymorphism
    //It could, however, be further developed to demand the DB records use
    //universal attributes (eg ID) but is not currently deemed necessary
    public interface IRecord
    {
    }
}
