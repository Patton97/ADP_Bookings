using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Views
{
    public interface IRecordGUI: IGUI
    {
        int[] GetSelectedIndices();
        int GetSelectedIndex();
        
        void SetSelectedIndices(int[] indices);
        void SetSelectedIndex(int index);
    }
}
