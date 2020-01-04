using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings.Views
{
    public interface IDepartmentGUI : IGUI
    {
        string CurrentDepartmentID { get; set; }
        string CurrentDepartmentName { get; set; }
        ListView.ListViewItemCollection DepartmentList { get; set; }
        ListView.ListViewItemCollection CurrentDepartmentBookings { get; set; }
        bool DepartmentList_Enabled { get; set; }
        bool CurrentDepartment_Enabled { get; set; }

        void Register(Presenters.DepartmentPresenter presenter);
    }    
}
