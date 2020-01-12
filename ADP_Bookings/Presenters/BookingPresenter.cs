using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to MVP structure reflected in folder/namespace heirarchy
using ADP_Bookings.Views;
using ADP_Bookings.Models;

namespace ADP_Bookings.Presenters
{
    public class BookingPresenter : RecordPresenter<Booking>
    {
        IBookingGUI screen; //view
        IBookingModel model;
        Department department; //The department the displayed bookings belong to

        public BookingPresenter(IBookingGUI screen, IBookingModel model, int departmentID) : base(screen)
        {
            this.screen = screen;
            screen.Register(this);
            this.model = model;
            this.department = FindDepartment(departmentID);            
            InitialiseForm();
        }
        public BookingPresenter(IBookingGUI screen, int departmentID) : this(screen, new BookingModel(), departmentID) { /* */ }

        public override void InitialiseForm()
        {
            //Assign title to form window
            screen.Text = "ADP: " + department.Company.Name + " > " + department.Name + " > Bookings";

            //Populate booking list
            LoadBookingList();

            //Initialise current booking panel
            ClearCurrentRecord();
        }

        void LoadBookingList()
        {
            screen.BookingList.Clear();
            records = GetAllBookingsFrom(department);
            foreach (Booking b in records)
            {
                ListViewItem lvi_booking = new ListViewItem(b.BookingID.ToString());
                lvi_booking.SubItems.Add(b.Name);
                lvi_booking.SubItems.Add(b.Date.ToString());
                lvi_booking.SubItems.Add(b.NumAttendees.ToString());
                lvi_booking.SubItems.Add("£" + b.EstimatedCost.ToString());
                lvi_booking.SubItems.Add("£" + b.ActualCost.ToString());
                screen.BookingList.Add(lvi_booking);
            }
        }

        protected override void LoadRecord(Booking selectedRecord)
        {
            //Clear old data
            ClearCurrentRecord();

            //Load in new data from selected booking
            this.selectedRecord = selectedRecord;
            screen.CurrentBookingID = selectedRecord.BookingID.ToString();
            screen.CurrentBookingName = selectedRecord.Name;
            screen.CurrentBookingDate = selectedRecord.Date;
            screen.CurrentBookingCost = decimal.Parse(selectedRecord.EstimatedCost.ToString());

            //Load Activities
            foreach (Activity a in selectedRecord.Activities)
            {
                ListViewItem lvi_activity = new ListViewItem(a.ActivityID.ToString());
                lvi_activity.SubItems.Add(a.Name);
                lvi_activity.SubItems.Add("£" + a.Cost.ToString());
                lvi_activity.SubItems.Add(a.Notes);
                screen.CurrentBookingActivities.Add(lvi_activity);
            }

            //Enabled user editing, reset tracker
            EnableCurrentBookingDisplay();
            ChangesPending = false;
        }
        protected override void LoadNewRecord() => LoadRecord(new Booking(0, "", DateTime.Today, 0, department));

        //Save department data back to database
        protected override void SaveRecord()
        {
            if (ChangesPending)
            {
                // Update any editable fields
                // NOTE: Future development pass could instead map the below and iterate
                selectedRecord.Name = screen.CurrentBookingName;
                selectedRecord.Date = screen.CurrentBookingDate;
                selectedRecord.EstimatedCost = float.Parse(screen.CurrentBookingCost.ToString());

                SaveBooking(selectedRecord);
            }

            //Reload form components to reflect changes
            ClearCurrentRecord();
            LoadBookingList();
        }

        protected override void ClearCurrentRecord()
        {
            //Disable user editing
            screen.CurrentBooking_Enabled = false;

            selectedRecord = null;
            screen.CurrentBookingID = "";
            screen.CurrentBookingName = "";
            screen.CurrentBookingDate = DateTime.Today;
            screen.CurrentBookingActivities.Clear();
            DisableCurrentBookingDisplay();

            //Reset editing tracker
            ChangesPending = false;
        }

        protected override void DeleteRecord()
        {
            //Reject attempts to delete non-existent records
            if (selectedRecord == null)
            {
                screen.ShowMessageBox("No booking selected.", "Cannot delete booking", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //selectedRecord is not null
            var confirmResult = screen.ShowMessageBox("This booking will be permenantly deleted.\nThis cannot be undone!",
                                                      "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                DeleteBooking(selectedRecord);

                //Reload form components to reflect changes
                ClearCurrentRecord();
                LoadBookingList();
            }
        }

        void EditActivities()
        {
            //Store selected index for reload when user returns to this form
            int idx = screen.GetSelectedIndex();
            screen.Hide();
            new Forms.frm_activities(selectedRecord.BookingID).ShowDialog();
            //NOTE: ShowDialog() means the below code won't resume untidl above form is closed

            //Force reload to reflect any changes made to DB in other form(s)
            LoadBookingList();
            LoadRecord(records[idx]);
            screen.SetSelectedIndex(idx);
            screen.Show();
        }

        //Control group toggling
        //Booking List Display
        void EnableBookingListDisplay() => screen.BookingList_Enabled = true;
        void DisableBookingListDisplay() => screen.BookingList_Enabled = false;
        //Current Booking Display
        void EnableCurrentBookingDisplay() => screen.CurrentBooking_Enabled = true;
        void DisableCurrentBookingDisplay() => screen.CurrentBooking_Enabled = false;

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Bookings List - new item selected
        public void lvw_Bookings_SelectedIndexChanged(int[] selectedIndices) => SelectRecord(selectedIndices);

        // Buttons
        public void btn_AddBooking_Click() => AddRecord();
        public void btn_DeleteBooking_Click() => DeleteRecord();
        public void btn_EditActivities_Click() => EditActivities();
        public void btn_ConfirmChanges_Click() => SaveRecord();
        public void btn_CancelChanges_Click() => CancelChanges();

        // Form is being closed
        public void frm_bookings_FormClosing(FormClosingEventArgs e) => CloseForm(e);

        // ********************************************************************************
        // Model (UoW) Communication ******************************************************
        // ********************************************************************************

        // Retrieve all records from Bookings table
        public List<Booking> GetAllBookings() => model.GetAllBookings();

        // Retrieve all bookings belonging to a specified department
        public List<Booking> GetAllBookingsFrom(Department department) => model.GetAllBookingsFrom(department);

        // Retrieve booking from a specified ID
        public Booking FindBooking(Booking booking) => model.FindBooking(booking);

        // Reports purely on the success/failure of booking retrieval
        public bool BookingExists(Booking booking) => model.FindBooking(booking) != null;

        // Save record in Bookings table - model will determine whether to Create/Update
        public void SaveBooking(Booking booking) => model.SaveBooking(booking);

        // Delete record from Bookings table
        public void DeleteBooking(Booking booking) => model.DeleteBooking(booking);

        // Retrieve Department from specified ID
        public Department FindDepartment(int departmentID) => model.FindDepartment(departmentID);
        public Department FindDepartment(Department department) => model.FindDepartment(department.DepartmentID);
    }
}
