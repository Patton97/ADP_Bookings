using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using ADP_Bookings.Presenters;
using ADP_Bookings.Views;

namespace ADP_Bookings.Forms
{
    public partial class frm_bookings : Form, IBookingGUI
    {
        private BookingPresenter presenter;
        public List<Control> ctrls_BookingList;
        public List<Control> ctrls_CurrentBooking;

        public string CurrentBookingID
        {
            get => txt_BookingID.Text;
            set => txt_BookingID.Text = value;
        }
        public string CurrentBookingName
        {
            get => txt_BookingName.Text;
            set => txt_BookingName.Text = value;
        }
        public DateTime CurrentBookingDate
        {
            get => dtp_BookingDate.Value;
            set => dtp_BookingDate.Value = value;
        }
        public ListView.ListViewItemCollection BookingList
        {
            get => lvw_Bookings.Items;
            set => lvw_Bookings.Items.AddRange(value);
        }
        public ListView.ListViewItemCollection CurrentBookingActivities
        {
            get => lvw_BookingActivities.Items;
            set => lvw_BookingActivities.Items.AddRange(value);
        }
        public bool BookingList_Enabled
        {
            get => ctrls_BookingList[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_BookingList.ForEach(ctrl => ctrl.Enabled = value);
        }
        public bool CurrentBooking_Enabled
        {
            get => ctrls_CurrentBooking[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_CurrentBooking.ForEach(ctrl => ctrl.Enabled = value);
        }

        public frm_bookings(Department department)
        {
            InitializeComponent();
            //Define groups of controls
            InitialiseControlGroups();

            //Assign presenter
            presenter = new BookingPresenter(this, department);
        }

        public void Register(BookingPresenter presenter) => this.presenter = presenter;

        private void frm_bookings_Load(object sender, EventArgs e) { /* */ }

        void InitialiseControlGroups()
        {
            //Controls relating to the full booking list display (left hand side)
            ctrls_BookingList = new List<Control>();
            ctrls_BookingList.Add(lbl_Bookings);
            ctrls_BookingList.Add(lvw_Bookings);
            ctrls_BookingList.Add(btn_AddBooking);

            //Controls relating to the current booking display (right hand side)
            ctrls_CurrentBooking = new List<Control>();
            ctrls_CurrentBooking.Add(lbl_BookingID);
            ctrls_CurrentBooking.Add(txt_BookingID);
            ctrls_CurrentBooking.Add(lbl_BookingName);
            ctrls_CurrentBooking.Add(txt_BookingName);
            ctrls_CurrentBooking.Add(lbl_BookingDate);
            ctrls_CurrentBooking.Add(dtp_BookingDate);
            ctrls_CurrentBooking.Add(lvw_BookingActivities);
            ctrls_CurrentBooking.Add(btn_ConfirmChanges);
            ctrls_CurrentBooking.Add(btn_CancelChanges);
        }

        public int GetSelectedBookingIndex() => lvw_Bookings.SelectedIndices[0];

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_companies
        private void lvw_Bookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.lvw_Bookings_SelectedIndexChanged(lvw_Bookings.SelectedIndices.Cast<int>().ToArray());
        }

        // Buttons
        private void btn_AddBooking_Click(object sender, EventArgs e) => presenter.btn_AddBooking_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();
    }
}
