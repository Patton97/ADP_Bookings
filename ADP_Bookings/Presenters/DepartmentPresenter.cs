using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using static ADP_Bookings.Models.DepartmentModel;
using ADP_Bookings.Views;

namespace ADP_Bookings.Presenters
{
    public class DepartmentPresenter : RecordPresenter<Department>
    {
        IDepartmentGUI screen; //view
        Company company; //The company the displayed departments belong to

        public DepartmentPresenter(IDepartmentGUI screen, Company company)
        {
            this.screen = screen;
            screen.Register(this);
            this.company = company;
            InitialiseForm();
        }

        protected override void InitialiseForm()
        {
            //Assign title to form window
            screen.Text = "ADP > " + company.Name + " > Departments";

            //Populate company list
            LoadDepartmentList();

            //Initialise current company panel
            ClearCurrentRecord();
        }

        void LoadDepartmentList()
        {
            screen.DepartmentList.Clear();
            records = GetAllDepartmentsFrom(company);
            foreach (Department d in records)
            {
                ListViewItem lvi_department = new ListViewItem(d.DepartmentID.ToString());
                lvi_department.SubItems.Add(d.Name);
                screen.DepartmentList.Add(lvi_department);
            }
        }

        protected override void LoadRecord(Department selectedRecord)
        {
            //Clear old data
            ClearCurrentRecord();

            //Load in new data from selected department
            this.selectedRecord = selectedRecord;
            screen.CurrentDepartmentID = selectedRecord.DepartmentID.ToString();
            screen.CurrentDepartmentName = selectedRecord.Name;

            //Load Department's Bookings
            foreach (Booking b in selectedRecord.Bookings)
            {
                ListViewItem lvi_booking = new ListViewItem(b.BookingID.ToString());
                lvi_booking.SubItems.Add(b.Name);
                lvi_booking.SubItems.Add(b.Date.ToString());
                lvi_booking.SubItems.Add(b.NumAttendees.ToString());
                lvi_booking.SubItems.Add(b.EstimatedCost.ToString());
                lvi_booking.SubItems.Add(b.ActualCost.ToString());
                screen.CurrentDepartmentBookings.Add(lvi_booking);
            }

            //Enabled user editing, reset tracking
            screen.CurrentDepartment_Enabled = true;
            ChangesPending = false;
        }
        protected override void LoadNewRecord() => LoadRecord(new Department(0, "", company));
        
        //Clear record (from screen) - local only, no DB operations
        protected override void ClearCurrentRecord()
        {
            //Disable user editing
            DisableCurrentDepartmentDisplay();

            selectedRecord = null;
            screen.CurrentDepartmentID = "";
            screen.CurrentDepartmentName = "";
            screen.CurrentDepartmentBookings.Clear();

            //Reset editing tracker
            ChangesPending = false;
        }

        //Save department record back to database
        protected override void SaveRecord()
        {
            // Update any editable fields
            // NOTE: Future development pass could instead map the below and iterate
            selectedRecord.Name = screen.CurrentDepartmentName;

            // Send to DB
            if (DepartmentExists(selectedRecord))
                UpdateDepartment(selectedRecord);
            else
                InsertNewDepartment(selectedRecord);

            // Reload form components to reflect changes
            ClearCurrentRecord();
            LoadDepartmentList();
        }

        protected override void DeleteRecord()
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("No department selected.", "Cannot delete department", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var confirmResult = MessageBox.Show("This department and all associated records will be permenantly deleted.\nThis cannot be undone!",
                                                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteDepartment(selectedRecord);

                    //Reload form components to reflect changes
                    ClearCurrentRecord();
                    LoadDepartmentList();
                }
            }
        }

        //Edit this department's bookings
        void EditBookings()
        {
            //Store selected index for reload when user returns to this form
            int idx = screen.GetSelectedIndex();
            screen.Hide();
            new Forms.frm_bookings(selectedRecord).ShowDialog();
            //NOTE: ShowDialog() means the below code won't resume until above form is closed

            //Force reload to reflect any changes made to DB in other form(s)
            LoadDepartmentList();
            LoadRecord(records[idx]);
            screen.SetSelectedIndex(idx);
            screen.Show();
        }

        //Control group toggling
        //Company List Display
        void EnableDepartmentListDisplay() => screen.DepartmentList_Enabled = true;
        void DisableDepartmentListDisplay() => screen.DepartmentList_Enabled = false;
        //Current Company Display
        void EnableCurrentDepartmentDisplay() => screen.CurrentDepartment_Enabled = true;
        void DisableCurrentDepartmentDisplay() => screen.CurrentDepartment_Enabled = false;

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_companies
        public void lvw_Departments_SelectedIndexChanged(int[] selectedIndices) => SelectRecord(selectedIndices);

        // Buttons
        public void btn_AddDepartment_Click() => AddRecord();
        public void btn_DeleteDepartment_Click() => DeleteRecord();
        public void btn_EditBookings_Click() => EditBookings();
        public void btn_ConfirmChanges_Click() => SaveRecord();
        public void btn_CancelChanges_Click() => CancelChanges();

        // Form is being closed
        public void frm_departments_FormClosing(FormClosingEventArgs e) => CloseForm(e);
    }
}
