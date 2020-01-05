﻿using System;
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
        public decimal CurrentBookingCost
        {
            get => nud_BookingCost.Value;
            set => nud_BookingCost.Value = value;
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
            InitialiseControlGroups();
            InitialiseChangeTracker();
            presenter = new BookingPresenter(this, department);
        }
        private void frm_bookings_Load(object sender, EventArgs e) { /* */ }

        //Used by presenter to register itself once succesfully constructed
        public void Register(BookingPresenter presenter) => this.presenter = presenter;
                
        //Control groups allow for mass-enabling/disabling of form controls
        //This prevents unauthorised editing and helps focus user attention
        //Long-winded, slightly ugly, but very useful!
        void InitialiseControlGroups()
        {
            //Controls relating to the full booking list display (left hand side)
            ctrls_BookingList = new List<Control>()
            {
                lbl_Bookings,
                lvw_Bookings,
                btn_AddBooking
            };

            //Controls relating to the current booking display (right hand side)
            ctrls_CurrentBooking = new List<Control>()
            {
                lbl_EditBooking,
                lbl_BookingID,
                txt_BookingID,
                lbl_BookingName,
                txt_BookingName,
                lbl_BookingDate,
                dtp_BookingDate,
                lbl_BookingCost,
                nud_BookingCost,
                lvw_BookingActivities,
                btn_EditBookings,
                btn_ConfirmChanges,
                btn_CancelChanges
            };
        }

        //To track if the user has begun to edit the open record,
        //  we create a custom EventHandler containing a delegate which updates a bool within the presenter
        //  then subscribe to any relevant events (TextChanged, CheckChanged, etc)
        //This proves very useful when there are several editable controls on the form (eg: Booking, Activity)
        //NOTE: Only the presenter itself can reset this to false.
        void InitialiseChangeTracker() //Admittedly poor function name, can't think of anything better
        {
            EventHandler handler = new EventHandler(delegate { presenter.NewChangePending(); });
            txt_BookingID.TextChanged += handler;
            txt_BookingName.TextChanged += handler;
            dtp_BookingDate.TextChanged += handler;
            dtp_BookingDate.ValueChanged += handler;
            nud_BookingCost.ValueChanged += handler;
        }

        public int[] GetSelectedIndices() => lvw_Bookings.SelectedIndices.Cast<int>().ToArray();
        public int GetSelectedIndex() => GetSelectedIndices()[0];

        // Allows presenter to manually override selected indices
        public void SetSelectedIndices(int[] indices) => Array.ForEach(indices, index => lvw_Bookings.Items[index].Selected = true);
        public void SetSelectedIndex(int index) => SetSelectedIndices(new int[] { index });

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Bookings List - new item selected
        private void lvw_Bookings_SelectedIndexChanged(object sender, EventArgs e) => presenter.lvw_Bookings_SelectedIndexChanged(GetSelectedIndices());

        // Buttons
        private void btn_AddBooking_Click(object sender, EventArgs e) => presenter.btn_AddBooking_Click();
        private void btn_DeleteBooking_Click(object sender, EventArgs e) => presenter.btn_DeleteBooking_Click();
        private void btn_EditActivities_Click(object sender, EventArgs e) => presenter.btn_EditActivities_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();

        // Form is being closed
        private void frm_bookings_FormClosing(object sender, FormClosingEventArgs e) => presenter.frm_bookings_FormClosing(e);
    }
}
