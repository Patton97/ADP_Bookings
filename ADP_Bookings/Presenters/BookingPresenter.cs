using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using static ADP_Bookings.Models.BookingModel;
using ADP_Bookings.Views;

namespace ADP_Bookings.Presenters
{
    public class BookingPresenter
    {
        IBookingGUI screen; //view
        List<Booking> bookings; //model
        Booking selectedBooking;
        Department department; //The department the displayed bookings belong to

        public BookingPresenter(IBookingGUI screen, Department department)
        {
            this.screen = screen;
            screen.Register(this);
            this.department = department;
            InitialiseForm();
        }

        void InitialiseForm()
        {
            //Assign title to form window
            screen.Text = "ADP Bookings > " + department.Company.Name + " > " + department.Name + " > Bookings";

            //Populate department list
            LoadBookingList();

            //Initialise current department panel
            screen.CurrentBookingID = "";
            screen.CurrentBookingName = "";
            screen.CurrentBookingDate = DateTime.Today;
            screen.BookingList = new ListView.ListViewItemCollection(new ListView());
            screen.CurrentBookingActivities = new ListView.ListViewItemCollection(new ListView());
            screen.CurrentBooking_Enabled = false;
        }

        void LoadBookingList()
        {
            screen.BookingList.Clear();
            bookings = GetAllBookingsFrom(department);
            foreach (Booking d in bookings)
            {
                ListViewItem lvi_booking = new ListViewItem(d.BookingID.ToString());
                lvi_booking.SubItems.Add(d.Name);
                screen.BookingList.Add(lvi_booking);
            }
        }

        void LoadBooking(Booking selectedBooking)
        {
            this.selectedBooking = selectedBooking;

            //Clear old data
            ClearCurrentBooking();

            //Load in new data from selected booking
            //Maybe switch from index to ID?
            screen.CurrentBookingID = selectedBooking.BookingID.ToString();
            screen.CurrentBookingName = selectedBooking.Name;

            //Load Bookings
            foreach (Activity a in selectedBooking.Activities)
            {
                ListViewItem lvi_activity = new ListViewItem(a.ActivityID.ToString());
                lvi_activity.SubItems.Add(a.Name);
                screen.CurrentBookingActivities.Add(lvi_activity);
            }

            //Enabled user editing
            screen.CurrentBooking_Enabled = true;
        }
        void LoadNewBooking() => LoadBooking(new Booking(0, "", DateTime.Today, department));

        //Save department data back to database
        void SaveBooking()
        {
            if (BookingExists(selectedBooking))
                UpdateBooking(new Booking(int.Parse(screen.CurrentBookingID), screen.CurrentBookingName, screen.CurrentBookingDate, department));
            else
                InsertNewBooking(new Booking(0, screen.CurrentBookingName, screen.CurrentBookingDate, department));
            
            //Reload form components to reflect changes
            ClearCurrentBooking();
            LoadBookingList();
        }

        void ClearCurrentBooking()
        {
            //Disable user editing
            screen.CurrentBooking_Enabled = false;

            //selectedDepartment = null;
            screen.CurrentBookingID = "";
            screen.CurrentBookingName = "";
            screen.CurrentBookingDate = DateTime.Today;
            screen.CurrentBookingActivities.Clear();
            DisableCurrentBookingDisplay();
        }

        //Uses ID of the last entry in the list to predict the booking's ID when saved
        //NOTE: value here is visual only, EF will decide what the actual value should be when adding record
        //Arguably, this produces false-positives and it might be better to show 0, or simply no value at all
        int PredictNextID()
        {
            try
            {
                return bookings[bookings.Count - 1].BookingID + 1;
            }
            catch (ArgumentOutOfRangeException ioore)
            {
                Console.WriteLine(ioore + ioore.StackTrace);
                return 0;
            }
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

        // Companies ListBox - lst_companies
        public void lvw_Bookings_SelectedIndexChanged(int[] selectedIndices)
        {
            //When using lvw.FullRowSelect == true, if the user changes rows
            //the list view first deselects the old row, then selects the new row
            //Therefore, we need to ignore the first 'dud' call
            if (selectedIndices.Length <= 0)
                return;
            //ListView also allows for multiple row selection. If this is the case,
            //the CurrentBooking section is wiped to avoid ambiguity
            if (selectedIndices.Length > 1)
                ClearCurrentBooking();
            else
                LoadBooking(bookings[selectedIndices[0]]);
        }

        // Buttons
        public void btn_AddBooking_Click()
        {
            //If a booking is already being edited
            if (screen.CurrentBooking_Enabled)
            {
                var confirmResult = MessageBox.Show("All changes will be lost!",
                                                    "Are you sure?",
                                                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.No)
                    return; //Exit function, continue previous behaviour
            }

            //If code reaches this point, either no booking is open or user
            //is happy to discard previous changes
            LoadNewBooking();
        }
        public void btn_ConfirmChanges_Click() => SaveBooking();
        public void btn_CancelChanges_Click()
        {
            var confirmResult = MessageBox.Show("All changes will be lost!",
                                                "Are you sure?",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                ClearCurrentBooking();
            //else, do nothing (continue previous behaviour)
        }
    }
}
