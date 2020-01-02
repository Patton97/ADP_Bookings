using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Views
{
    //contains a group of controls all GUIs (ie screens/forms/views)
    //can be guaranteed to have. Implementation is already provided within the winforms
    //controls by default, this interface just enables their invokation by presenters
    public interface IGUI
    {
        string Text { get; set; } //Title of the form window
        bool Visible { get; set; }
        void Hide();
        void Show();
    }
}
