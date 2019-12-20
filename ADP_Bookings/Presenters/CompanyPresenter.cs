using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using ADP_Bookings.Models;
using ADP_Bookings.Views;

namespace ADP_Bookings.Presenters
{
    public class CompanyPresenter
    {
        private ICompanyGUI screen; //view
        private List<Company> companies; //model
        private Company selectedCompany;

        public CompanyPresenter(ICompanyGUI screen)
        {
            this.screen = screen;
            screen.Register(this);
            InitialiseForm();
        }

        void InitialiseForm()
        {
            screen.CurrentCompanyID = "";
            screen.CurrentCompanyName = "";
            screen.CompanyList = new ListView.ListViewItemCollection(new ListView());
            screen.CurrentCompanyDepartments = new ListView.ListViewItemCollection(new ListView());

            //Populate company list
            LoadCompanyList();
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
            screen.CurrentCompany_Enabled = true;

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
        }
        void LoadNewCompany() => LoadCompany(new Company(0, ""));
        
        //Save company data back to database
        void SaveCompany()
        {
            if (CompanyExists(selectedCompany))
                UpdateCompany(new Company(int.Parse(screen.CurrentCompanyID), screen.CurrentCompanyName));
            else
                InsertNewCompany(new Company(int.Parse(screen.CurrentCompanyID), screen.CurrentCompanyName));

            //Reload Company List with changes
            LoadCompanyList();
        }

        void ClearCurrentCompany()
        {
            //selectedCompany = null;
            screen.CurrentCompanyID = "";
            screen.CurrentCompanyName = "";
            screen.CurrentCompanyDepartments.Clear();
            DisableCurrentCompanyDisplay();
        }

        //Control group toggling
        //Company List Display
        void EnableCompanyListDisplay() => screen.CompanyList_Enabled = true;
        void DisableCompanyListDisplay() => screen.CompanyList_Enabled = false;
        //Current Company Display
        void EnableCurrentCompanyDisplay() => screen.CurrentCompany_Enabled = true;
        void DisableCurrentCompanyDisplay() => screen.CurrentCompany_Enabled = false;

        // **************************************************
        // Data Retrieval ***********************************
        // **************************************************
        //
        // Should this be in here? Not sure
        // Should be in a "model" class?

        List<Company> GetAllCompanies()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Companies.GetAll(true).ToList();
            }
        }

        List<Company> GetAllCompaniesWithDepartments()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Companies.GetAll().ToList();
            }
        }

        List<Department> GetAllDepartmentsFrom(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Departments.GetDepartmentsFromCompany(company).ToList();
            }
        }
        Company FindCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Companies.Get(company.CompanyID);
            }
        }

        void InsertNewCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Companies.Add(company);
                unitOfWork.SaveChanges();
            }
        }

        bool CompanyExists(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Companies.Get(company.CompanyID) != null;
            }
        }

        void UpdateCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Companies.Update(company);
                unitOfWork.SaveChanges();
            }
        }

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_companies
        public void lvw_companies_SelectedIndexChanged(int[] selectedIndices)
        {
            //When using lvw.FullRowSelect == true, if the user changes rows
            //the list view first deselects the old row, then selects the new row
            //Therefore, we need to ignore the first 'dud' call
            if (selectedIndices.Length <= 0)
                return;
            //ListView also allows for multiple row selection. If this is the case,
            //the company details display should be wiped to avoid ambiguity
            if (selectedIndices.Length > 1)
                ClearCurrentCompany();
            else
                LoadCompany(companies[selectedIndices[0]]);
        }

        // Buttons
        public void btn_AddCompany_Click() => LoadNewCompany();
        public void btn_ConfirmChanges_Click() => SaveCompany();
        public void btn_CancelChanges_Click() => ClearCurrentCompany();
    }
}
