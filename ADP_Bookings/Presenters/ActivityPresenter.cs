using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to MVP structure reflected in folder/namespace heirarchy
using ADP_Bookings.Models;
using ADP_Bookings.Views;
using static ADP_Bookings.Models.ActivityModel; //allows static methods to be directly called

namespace ADP_Bookings.Presenters
{
    public class ActivityPresenter : RecordPresenter<Activity>
    {
        IActivityGUI screen; //view
        Booking booking; //The booking the displayed activities belong to

        public ActivityPresenter(IActivityGUI screen, Booking booking)
        {
            this.screen = screen;
            screen.Register(this);
            this.booking = booking;
            InitialiseForm();
        }

        protected override void InitialiseForm()
        {
            //Update form window title
            screen.Text = "ADP > " + booking.Department.Name + " > " + booking.Name + " > Activities";

            //Populate booking list
            LoadActivityList();

            //Initialise current booking panel
            ClearCurrentRecord();
        }

        void LoadActivityList()
        {
            screen.ActivityList.Clear();
            records = GetAllActivities();
            foreach (Activity a in records)
            {
                //ActivityList works different to other windows - first column is a checkbox
                //signfying whether the respective activity is part of the current booking
                ListViewItem lvi_activity = new ListViewItem();
                lvi_activity.SubItems.Add(a.ActivityID.ToString());
                lvi_activity.SubItems.Add(a.Name);
                lvi_activity.SubItems.Add("£" + a.Cost.ToString());
                lvi_activity.SubItems.Add(a.Notes);
                screen.ActivityList.Add(lvi_activity);
            }
        }

        protected override void LoadRecord(Activity selectedRecord)
        {
            //Clear old data
            ClearCurrentRecord();

            //Load in new data from selected activity
            this.selectedRecord = selectedRecord;
            screen.CurrentActivityID = selectedRecord.ActivityID.ToString();
            screen.CurrentActivityName = selectedRecord.Name;
            screen.CurrentActivityCost = decimal.Parse(selectedRecord.Cost.ToString());
            screen.CurrentActivityNotes = selectedRecord.Notes;

            //Enable user editing, reset tracker
            screen.CurrentActivity_Enabled = true;
            ChangesPending = false;
        }
        protected override void LoadNewRecord() => LoadRecord(new Activity(0, "", 0, ""));

        //Save activity data back to database
        protected override void SaveRecord()
        {
            // Update any editable fields
            // NOTE: Future development pass could instead map the below and iterate
            selectedRecord.Name = screen.CurrentActivityName;
            selectedRecord.Cost = float.Parse(screen.CurrentActivityCost.ToString());
            selectedRecord.Notes = screen.CurrentActivityNotes;

            // Send to DB
            if (ActivityExists(selectedRecord))
                UpdateActivity(selectedRecord);
            else
                InsertNewActivity(selectedRecord);

            // Reload form components to reflect changes
            ClearCurrentRecord();
            LoadActivityList();
        }

        protected override void ClearCurrentRecord()
        {
            //Disable user editing
            screen.CurrentActivity_Enabled = false;

            selectedRecord = null;
            screen.CurrentActivityID = "";
            screen.CurrentActivityName = "";
            screen.CurrentActivityCost = 0;
            screen.CurrentActivityNotes = "";
            DisableCurrentActivityDisplay();

            //Reset editing tracker
            ChangesPending = false;
        }

        protected override void DeleteRecord()
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("No booking selected.", "Cannot delete booking", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show("This activity will be deleted and removed from all bookings.\nThis cannot be undone!",
                                                "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                DeleteActivity(selectedRecord);

                //Reload form components to reflect changes
                ClearCurrentRecord();
                LoadActivityList();
            }
        }

        //Control group toggling
        //Activity List Display
        void EnableActivityListDisplay() => screen.ActivityList_Enabled = true;
        void DisableActivityListDisplay() => screen.ActivityList_Enabled = false;
        //Current Activity Display
        void EnableCurrentActivityDisplay() => screen.CurrentActivity_Enabled = true;
        void DisableCurrentActivityDisplay() => screen.CurrentActivity_Enabled = false;

        //This function is specific to Activity as it (currently) is the only 
        //disconnected table, where updates to other records need to be sent backwards
        //As such, it looks out of place and slightly breaks the structure of the program
        void UpdateBooking()
        {
            List<Activity> chosenActivities = new List<Activity>();
            foreach(int i in screen.GetChosenActivities())
            {
                booking.Activities.Add(FindActivity(records[i]));
            }
            BookingModel.UpdateBooking(booking);
        }

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Activities List - new item selected
        public void lvw_Activities_SelectedIndexChanged(int[] selectedIndices) => SelectRecord(selectedIndices);

        // Buttons
        public void btn_AddActivity_Click() => AddRecord();
        public void btn_UpdateBooking_Click() => UpdateBooking();
        public void btn_DeleteActivity_Click() => DeleteRecord();
        public void btn_ConfirmChanges_Click() => SaveRecord();
        public void btn_CancelChanges_Click() => CancelChanges();

        // Form is being closed
        public void frm_activities_FormClosing(FormClosingEventArgs e) => CloseForm(e);
    }
}