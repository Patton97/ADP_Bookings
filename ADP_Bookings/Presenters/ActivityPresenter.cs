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
    public class ActivityPresenter : RecordPresenter<Activity>
    {
        IActivityGUI screen; //view
        IActivityModel model;
        Booking booking; //The booking the displayed activities belong to

        public ActivityPresenter(IActivityGUI screen, IActivityModel model, int bookingID) : base(screen)
        {
            this.screen = screen;
            screen.Register(this);
            this.model = model;
            this.booking = FindBooking(bookingID);            
            InitialiseForm();
        }
        public ActivityPresenter(IActivityGUI screen, int bookingID) : this(screen, new ActivityModel(), bookingID) { /* */ }

        public override void InitialiseForm()
        {
            //Update form window title
            screen.Text = "ADP: " + booking.Department.Company.Name 
                        + " > " + booking.Department.Name 
                        + " > " + booking.Name 
                        + " > Activities";

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

                //Additional cycle to check if activity is present in booking record FK
                foreach (Activity activity in booking.Activities)
                {
                    if (activity.ActivityID == a.ActivityID)
                        lvi_activity.Checked = true;
                }

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
            if (ChangesPending)
            {
                // Update any editable fields
                // NOTE: Future development pass could instead map the below and iterate
                selectedRecord.Name = screen.CurrentActivityName;
                selectedRecord.Cost = float.Parse(screen.CurrentActivityCost.ToString());
                selectedRecord.Notes = screen.CurrentActivityNotes;

                // Send to DB
                SaveActivity(selectedRecord);
            }
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
                screen.ShowMessageBox("No booking selected.", "Cannot delete booking", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = screen.ShowMessageBox("This activity will be deleted and removed from all bookings.\nThis cannot be undone!",
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

        // ********************************************************************************
        // Model (UoW) Communication ******************************************************
        // ********************************************************************************

        // Retrieve all records from the Activities table
        public List<Activity> GetAllActivities() => model.GetAllActivities();

        // Retrieve activity from specified ID
        public Activity FindActivity(int activityID)    => model.FindActivity(activityID);
        public Activity FindActivity(Activity activity) => model.FindActivity(activity);

        // Reports purely success/failure of activity retrieval
        public bool ActivityExists(Activity activity) => model.ActivityExists(activity);

        public void SaveActivity(Activity activity) => model.SaveActivity(activity);

        // Delete record from Activities table
        public void DeleteActivity(Activity activity) => model.DeleteActivity(activity);

        // Retrieve booking from a specified ID
        public Booking FindBooking(int bookingID)   => model.FindBooking(bookingID);
        public Booking FindBooking(Booking booking) => model.FindBooking(booking);

        // This function is required as Activity records do not hold an FK to bookings which posses them
        // so any changes regarding CHOSEN activities need to be sent back to the Bookings table itself
        void UpdateBooking() => model.UpdateBooking(booking, screen.GetChosenActivities().ToList());
    }
}