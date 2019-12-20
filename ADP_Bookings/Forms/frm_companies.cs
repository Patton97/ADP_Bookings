using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using ADP_Bookings.Presenters;
using ADP_Bookings.Views;

namespace ADP_Bookings.Forms
{
    public partial class frm_companies : Form, ICompanyGUI
    {
        private CompanyPresenter presenter;
        public List<Control> ctrls_CompanyList;
        public List<Control> ctrls_CurrentCompany;

        public string CurrentCompanyID
        {
            get => txt_CompanyID.Text;
            set => txt_CompanyID.Text = value;
        }
        public string CurrentCompanyName
        {
            get => txt_CompanyName.Text;
            set => txt_CompanyName.Text = value;
        }
        public ListView.ListViewItemCollection CompanyList
        {
            get => lvw_companies.Items;
            set => lvw_companies.Items.AddRange(value);
        }
        public ListView.ListViewItemCollection CurrentCompanyDepartments
        {
            get => lvw_CompanyDepartments.Items;
            set => lvw_CompanyDepartments.Items.AddRange(value);
        }
        public bool CompanyList_Enabled
        {
            get => ctrls_CompanyList[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_CompanyList.ForEach(ctrl => ctrl.Enabled = value);
        }
        public bool CurrentCompany_Enabled
        {
            get => ctrls_CurrentCompany[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_CurrentCompany.ForEach(ctrl => ctrl.Enabled = value);
        }


        public frm_companies()
        {
            InitializeComponent();
            presenter = new CompanyPresenter(this);

            //Define groups of controls
            InitialiseControlGroups();
        }

        public void Register(CompanyPresenter presenter) => this.presenter = presenter;



        private void frm_companies_Load(object sender, EventArgs e)
        {

        }

        void InitialiseControlGroups()
        {
            //Controls relating to the full company list display (left hand side)
            ctrls_CompanyList = new List<Control>();
            ctrls_CompanyList.Add(lbl_Companies);
            ctrls_CompanyList.Add(lvw_companies);
            ctrls_CompanyList.Add(btn_AddCompany);

            //Controls relating to the current company display (right hand side)
            ctrls_CurrentCompany = new List<Control>();
            ctrls_CurrentCompany.Add(lbl_CompanyID);
            ctrls_CurrentCompany.Add(txt_CompanyID);
            ctrls_CurrentCompany.Add(lbl_CompanyName);
            ctrls_CurrentCompany.Add(txt_CompanyName);
            ctrls_CurrentCompany.Add(lvw_CompanyDepartments);
            ctrls_CurrentCompany.Add(btn_ConfirmChanges);
            ctrls_CurrentCompany.Add(btn_CancelChanges);
        }

        public int GetSelectedCompanyIndex() => lvw_companies.SelectedIndices[0];

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_companies
        private void lvw_companies_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.lvw_companies_SelectedIndexChanged(lvw_companies.SelectedIndices.Cast<int>().ToArray());
        }

        // Buttons
        private void btn_AddCompany_Click(object sender, EventArgs e) => presenter.btn_AddCompany_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();
    }
}
