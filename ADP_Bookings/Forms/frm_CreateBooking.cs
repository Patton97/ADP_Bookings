using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings
{
    public partial class frm_CreateBooking : Form
    {
        public frm_CreateBooking()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgv_bookings_int();
        }

        void dgv_bookings_int()
        {
            //dgv_bookings.DataSource = BookingRepository.GetCompanies();
        }
    }
}
