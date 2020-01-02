using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings.Views
{
    public interface IBookingGUI : IGUI
    {
        string CurrentBookingID { get; set; }
        string CurrentBookingName { get; set; }
        DateTime CurrentBookingDate { get; set; }
        ListView.ListViewItemCollection BookingList { get; set; }
        ListView.ListViewItemCollection CurrentBookingActivities { get; set; }
        bool BookingList_Enabled { get; set; }
        bool CurrentBooking_Enabled { get; set; }

        void Register(Presenters.BookingPresenter presenter);

        int GetSelectedBookingIndex();
    }
}
