using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using static ADP_Bookings.Models.CompanyModel;
using ADP_Bookings.Views;

namespace ADP_Bookings.Presenters
{
    public class CompanyPresenter
    {
        ICompanyGUI screen; //view
        List<Company> companies; //model
        Company selectedCompany;
        public bool CurrentCompanyEdited = false; //Has the user begun editing the record yet

        public CompanyPresenter(ICompanyGUI screen)
        {
            this.screen = screen;
            screen.Register(this);
            InitialiseForm();
        }

        void InitialiseForm()
        {
            //Assign title to form window
            screen.Text = "ADP > Companies";

            //Populate company list
            LoadCompanyList();

            //Initialise current company panel
            screen.CurrentCompanyID = "";
            screen.CurrentCompanyName = "";
            screen.CompanyList = new ListView.ListViewItemCollection(new ListView());
            screen.CurrentCompanyDepartments = new ListView.ListViewItemCollection(new ListView());
            screen.CurrentCompany_Enabled = false;
        }

        void LoadCompanyList()
        {
            screen.CompanyList.Clear();
            companies = GetAllCompanies();
            foreach (Company c in companies)
            {
                ListViewItem lvi_company = new ListViewItem(c.CompanyID.ToString());
                lvi_company.SubItems.Add(c.Name);
                screen.CompanyList.Add(lvi_company);
            }
        }

        void LoadCompany(Company selectedCompany)
        {
            this.selectedCompany = selectedCompany;

            //Clear old data
            ClearCurrentCompany();

            //Load in new data from selected company
            //Maybe switch from index to ID?
            screen.CurrentCompanyID = selectedCompany.CompanyID.ToString();
            screen.CurrentCompanyName = selectedCompany.Name;

            //Load Departments
            foreach (Department d in selectedCompany.Departments)
            {
                ListViewItem lvi_department = new ListViewItem(d.DepartmentID.ToString());
                lvi_department.SubItems.Add(d.Name);
                screen.CurrentCompanyDepartments.Add(lvi_department);
            }

            //Enabled user editing
            screen.CurrentCompany_Enabled = true;
        }
        void LoadNewCompany() => LoadCompany(new Company(0, ""));
        
        //Save company data back to database
        void SaveCompany()
        {
            if (CompanyExists(selectedCompany))
                UpdateCompany(new Company(int.Parse(screen.CurrentCompanyID), screen.CurrentCompanyName));
            else
                InsertNewCompany(new Company(int.Parse(screen.CurrentCompanyID), screen.CurrentCompanyName));

            //Reload form components to reflect changes
            ClearCurrentCompany();
            LoadCompanyList();
        }

        void ClearCurrentCompany()
        {
            //Disable user editing
            screen.CurrentCompany_Enabled = false;

            //selectedCompany = null;
            screen.CurrentCompanyID = "";
            screen.CurrentCompanyName = "";
            screen.CurrentCompanyDepartments.Clear();
            DisableCurrentCompanyDisplay();
            EnableCompanyListDisplay();
        }

        void EditDepartments()
        {
            screen.Hide();
            new Forms.frm_departments(selectedCompany).ShowDialog();
            //NOTE: ShowDialog() means the below code won't resume until above form is closed

            //Force reload to reflect any changes made in other form(s)
            LoadCompanyList();
            LoadCompany(selectedCompany);
            screen.Show();            
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

        // Companies ListBox - lst_companies
        public void lvw_companies_SelectedIndexChanged(int[] selectedIndices)
        {
            //First, check if company is currently being edited
            if(screen.CurrentCompany_Enabled)
            {
                var confirmResult = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                    SaveCompany();
                else if (confirmResult == DialogResult.No)
                    ClearCurrentCompany();
                else if (confirmResult == DialogResult.Cancel)
                    return; //Exit function, resume previous behaviour
            }
            //If code reaches this point, no company is currently being edited

            //When using ListView with FullRowSelect, if the user changes rows
            //the list view first deselects the old row, then selects the new row
            //Therefore, we ignore the first 'dud' call where no rows are selected
            if (selectedIndices.Length <= 0)
                return; //Exit function, resume previous behaviour

            //ListView also allows for multiple row selection. 
            //If this is the case, the company details display is wiped to avoid ambiguity
            if (selectedIndices.Length > 1)
                ClearCurrentCompany();
            else
                LoadCompany(companies[selectedIndices[0]]);
        }

        // Buttons
        public void btn_AddCompany_Click()
        {
            //If a company is already being edited
            if(screen.CurrentCompany_Enabled)
            {
                var confirmResult = MessageBox.Show("All changes will be lost!", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                    LoadNewCompany();
            }
            //else, do nothing (continue previous behaviour)
        }

        public void btn_DeleteCompany_Click()
        {
            if(selectedCompany == null)
            {
                MessageBox.Show("No company selected.","Cannot delete company", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var confirmResult = MessageBox.Show("This company and all associated records will be permenantly deleted.\nThis cannot be undone!",
                                                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteCompany(selectedCompany);

                    //Reload form components to reflect changes
                    ClearCurrentCompany();
                    LoadCompanyList();
                }
            }            
        }

        public void btn_EditDepartments_Click() => EditDepartments();
        public void btn_ConfirmChanges_Click() => SaveCompany();
        public void btn_CancelChanges_Click()
        {
            var confirmResult = MessageBox.Show("All changes will be lost!", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
                ClearCurrentCompany();
            //else, do nothing (continue previous behaviour)
        }        
    }
}
