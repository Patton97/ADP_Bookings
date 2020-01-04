using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings.Views
{
    public interface IActivityGUI : IGUI
    {
        string CurrentActivityID { get; set; }
        string CurrentActivityName { get; set; }
        decimal CurrentActivityCost { get; set; }
        string CurrentActivityNotes { get; set; }
        ListView.ListViewItemCollection ActivityList { get; set; }
        bool ActivityList_Enabled { get; set; }
        bool CurrentActivity_Enabled { get; set; }

        void Register(Presenters.ActivityPresenter presenter);

        int GetSelectedActivityIndex();
    }
}
