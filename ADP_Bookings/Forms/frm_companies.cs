using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings.Forms
{
    public partial class frm_companies : Form, ICompanyGUI
    {
        private CompanyPresenter presenter;
        List<Company> companies = new List<Company>();
        Company selectedCompany;

        public frm_companies()
        {
            InitializeComponent();
        }

        public void Register(CompanyPresenter presenter) => this.presenter = presenter;

        private void frm_companies_Load(object sender, EventArgs e)
        {
            dgv_companies_init();
            lvw_CompanyDepartments_init();
        }

        void dgv_companies_init()
        {
            companies = GetAllCompaniesWithDepartments();
            foreach(Company c in companies)
            {
                lst_companies.Items.Add(c.CompanyID + "\t" + c.Name);
            }
        }

        void lvw_CompanyDepartments_init()
        {
            lvw_CompanyDepartments.View = View.Details;
            lvw_CompanyDepartments.Columns.Add("ID");
            lvw_CompanyDepartments.Columns.Add("Name");
        }


        void LoadCompany()
        {
            selectedCompany = companies[lst_companies.SelectedIndex];
            txt_CompanyID.Text = selectedCompany.CompanyID.ToString();
            txt_CompanyName.Text = selectedCompany.Name;
            
            //Load Departments
            foreach (Department d in GetAllDepartmentsFrom(selectedCompany))
            {
                ListViewItem lvi_department = new ListViewItem(d.DepartmentID.ToString());
                lvi_department.SubItems.Add(d.Name);
                lvw_CompanyDepartments.Items.Add(lvi_department);
            }
        }

        void LoadNewCompany()
        {
            selectedCompany = null;
            txt_CompanyID.Text = "0";
            txt_CompanyName.Text = "";

            //Load Departments            
        }

        void SaveCompany()
        {
            if (selectedCompany == null)
            {
                InsertNewCompany(new Company(int.Parse(txt_CompanyID.Text), txt_CompanyName.Text));
            }
            else
            {
                //Update
            }
        }

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************

        // Companies DataGridView - dgv_companies
        private void dgv_companies_CellClick(object sender, DataGridViewCellEventArgs e) => LoadCompany();
        private void dgv_companies_CellContentClick(object sender, DataGridViewCellEventArgs e) => LoadCompany();
        private void dgv_companies_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) => LoadCompany();
        private void dgv_companies_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => LoadCompany();

        // Companies ListBox - lst_companies
        private void lst_companies_SelectedIndexChanged(object sender, EventArgs e) => LoadCompany();

        /**************************************************/

        //Move to separate class within MVP
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

        void InsertNewCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Companies.Add(company);
                unitOfWork.SaveChanges();
            }
        }

        private void btn_AddCompany_Click(object sender, EventArgs e) => LoadNewCompany();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => SaveCompany();
    }
}
