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
        Booking booking; //The booking the displayed activities belong to

        public ActivityPresenter(IActivityGUI screen, int bookingID)
        {
            this.screen = screen;
            screen.Register(this);
            this.booking = FindBooking(bookingID);
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
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                List<Activity> activities = unitOfWork.Bookings.Get(booking.BookingID).Activities.ToList();
                //First remove all activities from DB record
                foreach(Activity a in activities)
                {
                    unitOfWork.Bookings.Get(booking.BookingID).Activities.Remove(a);
                }
                
                //Then add new selection
                foreach (int i in screen.GetChosenActivities())
                {
                    unitOfWork.Bookings.Get(booking.BookingID).Activities.Add(unitOfWork.Activities.Get(records[i].ActivityID));
                }

                unitOfWork.SaveChanges();
            }
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

        // ********************************************************************************
        // Model (UoW) Communication ******************************************************
        // ********************************************************************************

        // Static classes used because they solely act as a communication window to the UoW
        // NOTE: Not all functions here are necessarily used by the current application,
        //       their inclusion is in anticipation of future development requirements
        // NOTE: Originally stored in separate classes (CompanyModel.cs, etc)
        //       but moved to presenter to reflect format given in week 5 lecture slides

        // Creates new record in Activities table
        public static void InsertNewActivity(Activity activity)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Activities.Add(activity);
                unitOfWork.SaveChanges();
            }
        }

        // Retrieve all records from the Activities table
        public static List<Activity> GetAllActivities()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Activities.GetAll().ToList();
            }
        }

        // Retrieve activity from specified ID
        public static Activity FindActivity(int activityID)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Activities.Get(activityID);
            }
        }
        public static Activity FindActivity(Activity activity) => FindActivity(activity.ActivityID);

        // Reports purely success/failure of activity retrieval
        public static bool ActivityExists(Activity activity) => FindActivity(activity) != null;

        // Update existing record in Activities table
        public static void UpdateActivity(Activity activity)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Activities.Update(activity);
                unitOfWork.SaveChanges();
            }
        }

        // Delete record from Activities table
        public static void DeleteActivity(Activity activity)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Activities.Remove(unitOfWork.Activities.Get(activity.ActivityID));
                unitOfWork.SaveChanges();
            }
        }

        /* NOTE: This function seems out of place, but this is due *
         *       to the activity form needing to update the booking*
         *       since activity records are 'static' ie have no FK *
         *       relating to the booking(s) they are listed upon.  *
         * NOTE: Could probably be reworked to send this info back *
         *       to the booking form, and then update the record.  */
        // Retrieve booking from a specified ID
        public static Booking FindBooking(int bookingID)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Bookings.Get(bookingID);
            }
        }
        public static Booking FindBooking(Booking booking) => FindBooking(booking.BookingID);
    }
}