using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings.Views
{
    public interface ICompanyGUI : IGUI
    {
        string CurrentCompanyID { get; set; }
        string CurrentCompanyName { get; set; }
        ListView.ListViewItemCollection CompanyList { get; set; }
        ListView.ListViewItemCollection CurrentCompanyDepartments { get; set; }
        bool CompanyList_Enabled { get; set; }
        bool CurrentCompany_Enabled { get; set; }

        void Register(Presenters.CompanyPresenter presenter);

    }
}
