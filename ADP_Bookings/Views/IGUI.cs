using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ADP_Bookings.Presenters;

namespace ADP_Bookings.Views
{
    //Components all GUIs (ie screens/forms/views) are guaranteed to have
    //Implementation for most components promised here is provided by default within 
    //System.Windows.Forms, this interface just enables their invokation by presenters
    public interface IGUI
    {
        //Existing WinForm components
        string Text { get; set; } //Title of the form window
        bool Visible { get; set; }
        void Hide();
        void Show();
        //void Register(Presenter presenter);
    }
}
