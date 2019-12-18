using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Views
{
    public interface ICompanyGUI
    {
        string currentCompanyID { get; set; }
        string currentCompanyName { get; set; }
        void Register(Presenters.CompanyPresenter presenter);
    }
}
