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
    public class DepartmentPresenter
    {
        IDepartmentGUI screen; //view
        List<Department> departments; //model
        Department selectedDepartment;
        Company company; //The company the displayed departments belong to
        public bool CurrentDepartmentEdited = false; //Has the user begun editing the record yet

        public DepartmentPresenter(IDepartmentGUI screen, Company company)
        {
            this.screen = screen;
            screen.Register(this);
            this.company = company;
            InitialiseForm();
        }

        void InitialiseForm()
        {
            //Assign title to form window
            screen.Text = "ADP > " + company.Name + " > Departments";

            //Populate company list
            LoadDepartmentList();

            //Initialise current company panel
            screen.CurrentDepartmentID = "";
            screen.CurrentDepartmentName = "";
            screen.DepartmentList = new ListView.ListViewItemCollection(new ListView());
            screen.CurrentDepartmentBookings = new ListView.ListViewItemCollection(new ListView());
            screen.CurrentDepartment_Enabled = false;
        }

        void LoadDepartmentList()
        {
            screen.DepartmentList.Clear();
            departments = GetAllDepartmentsFrom(company);
            foreach (Department d in departments)
            {
                ListViewItem lvi_department = new ListViewItem(d.DepartmentID.ToString());
                lvi_department.SubItems.Add(d.Name);
                screen.DepartmentList.Add(lvi_department);
            }
        }

        void LoadDepartment(Department selectedDepartment)
        {
            this.selectedDepartment = selectedDepartment;

            //Clear old data
            ClearCurrentDepartment();

            //Load in new data from selected department
            //Maybe switch from index to ID?
            screen.CurrentDepartmentID = selectedDepartment.DepartmentID.ToString();
            screen.CurrentDepartmentName = selectedDepartment.Name;

            //Load Bookings
            foreach (Booking b in selectedDepartment.Bookings)
            {
                ListViewItem lvi_booking = new ListViewItem(b.BookingID.ToString());
                lvi_booking.SubItems.Add(b.Name);
                lvi_booking.SubItems.Add(b.Date.ToString());
                lvi_booking.SubItems.Add(b.NumAttendees.ToString());
                lvi_booking.SubItems.Add(b.EstimatedCost.ToString());
                lvi_booking.SubItems.Add(b.ActualCost.ToString());
                screen.CurrentDepartmentBookings.Add(lvi_booking);
            }

            //Enabled user editing
            screen.CurrentDepartment_Enabled = true;
        }
        void LoadNewDepartment() => LoadDepartment(new Department(0, "", company));

        //Save company data back to database
        void SaveDepartment()
        {
            if (DepartmentExists(selectedDepartment))
                UpdateDepartment(new Department(int.Parse(screen.CurrentDepartmentID), screen.CurrentDepartmentName, company));
            else
                InsertNewDepartment(new Department(0, screen.CurrentDepartmentName, company));

            //Reload form components to reflect changes
            ClearCurrentDepartment();
            LoadDepartmentList();
        }

        void ClearCurrentDepartment()
        {
            //Disable user editing
            screen.CurrentDepartment_Enabled = false;

            //selectedCompany = null;
            screen.CurrentDepartmentID = "";
            screen.CurrentDepartmentName = "";
            screen.CurrentDepartmentBookings.Clear();
            DisableCurrentDepartmentDisplay();
        }

        void EditBookings()
        {
            screen.Hide();
            new Forms.frm_bookings(selectedDepartment).ShowDialog();
            //NOTE: ShowDialog() means the below code won't resume until above form is closed

            //Force reload to reflect any changes made in other form(s)
            LoadDepartmentList();
            LoadDepartment(selectedDepartment);
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
        public void lvw_Departments_SelectedIndexChanged(int[] selectedIndices)
        {
            //First, check if department is currently being edited
            if (screen.CurrentDepartment_Enabled)
            {
                var confirmResult = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                    SaveDepartment();
                else if (confirmResult == DialogResult.No)
                    ClearCurrentDepartment();
                else if (confirmResult == DialogResult.Cancel)
                    return; //Exit function, resume previous behaviour
            }
            //If code reaches this point, no department is currently being edited

            //When using ListView with FullRowSelect, if the user changes rows
            //the list view first deselects the old row, then selects the new row
            //Therefore, we ignore the first 'dud' call where no rows are selected
            if (selectedIndices.Length <= 0)
                return; //Exit function, resume previous behaviour

            //ListView also allows for multiple row selection. 
            //If this is the case, the company details display is wiped to avoid ambiguity
            if (selectedIndices.Length > 1)
                ClearCurrentDepartment();
            else
                LoadDepartment(departments[selectedIndices[0]]);
        }

        // Buttons
        public void btn_AddDepartment_Click()
        {
            //If a company is already being edited
            if (screen.CurrentDepartment_Enabled)
            {
                var confirmResult = MessageBox.Show("All changes will be lost!", "Are you sure?", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                    LoadNewDepartment();
            }
            //else, do nothing (continue previous behaviour)
        }

        public void btn_DeleteDepartment_Click()
        {
            if (selectedDepartment == null)
            {
                MessageBox.Show("No department selected.", "Cannot delete department", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var confirmResult = MessageBox.Show("This department and all associated records will be permenantly deleted.\nThis cannot be undone!",
                                                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteDepartment(selectedDepartment);

                    //Reload form components to reflect changes
                    ClearCurrentDepartment();
                    LoadDepartmentList();
                }
            }
        }

        public void btn_EditBookings_Click() => EditBookings();
        public void btn_ConfirmChanges_Click() => SaveDepartment();
        public void btn_CancelChanges_Click()
        {
            var confirmResult = MessageBox.Show("All changes will be lost!", "Are you sure?", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                ClearCurrentDepartment();
            //else, do nothing (continue previous behaviour)
        }

    }
}
