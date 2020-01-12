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
    public class DepartmentPresenter : RecordPresenter<Department>
    {
        IDepartmentGUI screen; //view
        IDepartmentModel model; //model
        Company company; //The company the displayed departments belong to

        public DepartmentPresenter(IDepartmentGUI screen, IDepartmentModel model, int companyID ) : base(screen)
        {
            this.screen = screen;
            screen.Register(this);
            this.model = model;
            this.company = model.FindCompany(companyID);
            InitialiseForm();
        }
        public DepartmentPresenter(IDepartmentGUI screen, int companyID) : this(screen, new DepartmentModel(), companyID) { /* */ }

        public override void InitialiseForm()
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
            // Only perform save if record has been changed
            if (ChangesPending)
            {
                // Update any editable fields
                selectedRecord.Name = screen.CurrentDepartmentName;

                // Send to Model
                SaveDepartment(selectedRecord);
            }

            // Reload form components to reflect changes
            ClearCurrentRecord();
            LoadDepartmentList();
        }

        protected override void DeleteRecord()
        {
            if (selectedRecord == null)
            {
                screen.ShowMessageBox("No department selected.", "Cannot delete department", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var confirmResult = screen.ShowMessageBox("This department and all associated records will be permenantly deleted.\nThis cannot be undone!",
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
            new Forms.frm_bookings(selectedRecord.DepartmentID).ShowDialog();
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

        // ********************************************************************************
        // Model (UoW) Communication ******************************************************
        // ********************************************************************************

        // Static classes used because they solely act as a communication window to the UoW
        // NOTE: Not all functions here are necessarily used by the current application,
        //       their inclusion is in anticipation of future development requirements
        // NOTE: Originally stored in separate classes (DepartmentModel.cs, etc)
        //       but moved to presenter to reflect format given in week 5 lecture slides
        
        // Retrieve all records in Departments table
        public List<Department> GetAllDepartments() => model.GetAllDepartments();

        // Retrieve all departments belonging to a specfied company
        public List<Department> GetAllDepartmentsFrom(Company company) => model.GetAllDepartmentsFrom(company);

        // Retrieve Department from specified ID
        public Department FindDepartment(int departmentID) => model.FindDepartment(departmentID);
        public Department FindDepartment(Department department) => FindDepartment(department.DepartmentID);

        // Reports purely success/failure of company retrieval
        public bool DepartmentExists(Department department) => model.DepartmentExists(department);

        // Delete record from Departments table
        public void DeleteDepartment(Department department) => model.DeleteDepartment(department);

        // Save record in Departments table - model will determine whether to Create/Update
        public void SaveDepartment(Department department) => model.SaveDepartment(department);

        // Retrieve company from specified ID
        // NOTE: These methods cause DepartmentPresenter to be couple with CompanyModel
        //public Company FindCompany(int companyID) => CompanyModel.FindCompany(companyID);
        //public Company FindCompany(Company company) => CompanyModel.FindCompany(company);
        //public Company FindCompany(Company company) => CompanyModel.FindCompany(company);
    }
}
