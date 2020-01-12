using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings.Views
{
    public interface IRecordGUI: IGUI
    {
        int[] GetSelectedIndices();
        int GetSelectedIndex();
        
        void SetSelectedIndices(int[] indices);
        void SetSelectedIndex(int index);

        // Allows MessageBox calls to be abstracted to View layer only
        // Also means tests do not *actually* show a messagebox
        DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}
