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
    public class CompanyPresenter : RecordPresenter<Company> 
    {
        ICompanyGUI screen; //view
        ICompanyModel model;

        public CompanyPresenter(ICompanyGUI screen, ICompanyModel model) : base(screen)
        {
            this.screen = screen;
            screen.Register(this);
            this.model = model;
            InitialiseForm();
        }
        public CompanyPresenter(ICompanyGUI screen) : this(screen, new CompanyModel()) { /* */}

        public override void InitialiseForm()
        {
            //Assign title to form window
            screen.Text = "ADP > Companies";

            //Populate company list
            LoadCompanyList();

            //Initialise current department panel
            ClearCurrentRecord();
        }

        void LoadCompanyList()
        {
            screen.CompanyList.Clear();
            records = GetAllCompanies();
            foreach (Company c in records)
            {
                ListViewItem lvi_company = new ListViewItem(c.CompanyID.ToString());
                lvi_company.SubItems.Add(c.Name);
                screen.CompanyList.Add(lvi_company);
            }
        }

        protected override void LoadRecord(Company selectedRecord)
        {
            //Clear old data
            ClearCurrentRecord();

            //Load in new data from selected company
            this.selectedRecord = selectedRecord;
            screen.CurrentCompanyID = selectedRecord.CompanyID.ToString();
            screen.CurrentCompanyName = selectedRecord.Name;

            //Load Departments
            foreach (Department d in selectedRecord.Departments)
            {
                ListViewItem lvi_department = new ListViewItem(d.DepartmentID.ToString());
                lvi_department.SubItems.Add(d.Name);
                screen.CurrentCompanyDepartments.Add(lvi_department);
            }

            //Enable user editing, reset tracking
            EnableCurrentCompanyDisplay();
            ChangesPending = false;

        }
        protected override void LoadNewRecord() => LoadRecord(new Company(0, ""));

        //Clear record (from screen) - local only, no DB operations
        protected override void ClearCurrentRecord()
        {
            //Disable user editing
            DisableCurrentCompanyDisplay();

            selectedRecord = null;
            screen.CurrentCompanyID = "";
            screen.CurrentCompanyName = "";
            screen.CurrentCompanyDepartments.Clear();

            //Reset editing tracker
            ChangesPending = false;
        }
        
        protected override void DeleteRecord()
        {
            if (selectedRecord == null)
            {
                screen.ShowMessageBox("No company selected.", "Cannot delete company", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = screen.ShowMessageBox("This company and all associated records will be permenantly deleted.\nThis cannot be undone!",
                                                  "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                DeleteCompany(selectedRecord);

                //Reload form components to reflect changes
                ClearCurrentRecord();
                LoadCompanyList();
            }
        }

        void EditDepartments()
        {
            //Store selected index for reload when user returns to this form
            int idx = screen.GetSelectedIndex();
            screen.Hide();
            new Forms.frm_departments(selectedRecord.CompanyID).ShowDialog();
            //NOTE: ShowDialog() means the below code won't resume until above form is closed

            //Force reload to reflect any changes made to DB in other form(s)
            LoadCompanyList();
            LoadRecord(records[idx]);
            screen.SetSelectedIndex(idx);
            screen.Show();
        }

        //Save company data back to database
        protected override void SaveRecord()
        {
            // Only perform save if record has been changed
            if (ChangesPending)
            {
                // Update any editable fields
                selectedRecord.Name = screen.CurrentCompanyName;

                // Send to Model
                SaveCompany(selectedRecord);
            }

            // Reload form components to reflect changes
            ClearCurrentRecord();
            LoadCompanyList();
        }

        //Control group toggling
        //Company List Display
        void EnableCompanyListDisplay() => screen.CompanyList_Enabled = true;
        void DisableCompanyListDisplay() => screen.CompanyList_Enabled = false;
        //Current Company Display
        void EnableCurrentCompanyDisplay() => screen.CurrentCompany_Enabled = true;
        void DisableCurrentCompanyDisplay() => screen.CurrentCompany_Enabled = false;

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_records
        public void lvw_companies_SelectedIndexChanged(int[] selectedIndices) => SelectRecord(selectedIndices);

        // Buttons
        public void btn_AddCompany_Click() => AddRecord();
        public void btn_DeleteCompany_Click() => DeleteRecord();
        public void btn_EditDepartments_Click() => EditDepartments();
        public void btn_ConfirmChanges_Click() => SaveRecord();
        public void btn_CancelChanges_Click() => CancelChanges();

        // Form is being closed
        public void frm_companies_FormClosing(FormClosingEventArgs e) => CloseForm(e);

        // ********************************************************************************
        // Model (UoW) Communication ******************************************************
        // ********************************************************************************        

        // Retrieve all records in Companies table
        public List<Company> GetAllCompanies() => model.GetAllCompanies();

        // Retrieve company from specified ID
        public Company FindCompany(Company company) => model.FindCompany(company.CompanyID);

        // Reports purely success/failure of company retrieval
        public bool CompanyExists(Company company) => model.CompanyExists(company);

        // Save record in Companies table - model will determine whether to Create/Update
        public void SaveCompany(Company company) => model.SaveCompany(company);

        // Delete record in Companies table
        public void DeleteCompany(Company company) => model.DeleteCompany(company);        
    }
}