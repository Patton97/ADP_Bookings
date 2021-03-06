﻿//16007006 Andrew Patton
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
        public List<EventHandler> ctrls_Editable; //Helps track whether record has been edited

        // ********************************************************************************
        // View Properties ****************************************************************
        // ******************************************************************************** 

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
            get => ctrls_CompanyList.First().Enabled; //We assume if first is enabled, all are
            set => ctrls_CompanyList.ForEach(ctrl => ctrl.Enabled = value);
        }
        public bool CurrentCompany_Enabled
        {
            get => ctrls_CurrentCompany.First().Enabled; //We assume if first is enabled, all are
            set => ctrls_CurrentCompany.ForEach(ctrl => ctrl.Enabled = value);
        }

        // ********************************************************************************
        // Form Functions *****************************************************************
        // ******************************************************************************** 

        public frm_companies()
        {
            InitializeComponent();
            InitialiseControlGroups();
            InitialiseChangeTracker();
            presenter = new CompanyPresenter(this);
        }
        private void frm_companies_Load(object sender, EventArgs e) => presenter.InitialiseForm();

        //Used by presenter to register itself once succesfully constructed
        public void Register(CompanyPresenter presenter) => this.presenter = presenter;

        //Control groups allow for mass-enabling/disabling of form controls
        void InitialiseControlGroups()
        {
            //Controls relating to the full company list display (left hand side)
            ctrls_CompanyList = new List<Control>()
            {
                lbl_Companies,
                lvw_companies,
                btn_AddCompany,
                btn_DeleteCompany
            };

            //Controls relating to the current company display (right hand side)
            ctrls_CurrentCompany = new List<Control>()
            {
                lbl_EditCompany,
                lbl_CompanyID,
                txt_CompanyID,
                lbl_CompanyName,
                txt_CompanyName,
                lbl_CompanyDepartments,
                lvw_CompanyDepartments,
                btn_ConfirmChanges,
                btn_CancelChanges,
                btn_EditDepartments
            };
        }

        // To track if the user has begun to edit the open record
        // NOTE: Only the presenter itself can reset this to false.
        void InitialiseChangeTracker()
        {
            EventHandler tracker = new EventHandler(delegate { presenter.NewChangePending(); });
            txt_CompanyID.TextChanged += tracker;
            txt_CompanyName.TextChanged += tracker;
        }

        //Encapsulates MessageBox calls in View - rather than calling it from Presenter layer
        public DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }

        // Utility functions to retrieve currently selected list indices
        public int[] GetSelectedIndices() => lvw_companies.SelectedIndices.Cast<int>().ToArray();
        public int GetSelectedIndex() => GetSelectedIndices()[0];

        // Allows presenter to manually override selected indices
        public void SetSelectedIndices(int[] indices) => Array.ForEach(indices, index => lvw_companies.Items[index].Selected = true);
        public void SetSelectedIndex(int index) => SetSelectedIndices(new int[]{index});


        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_companies
        private void lvw_companies_SelectedIndexChanged(object sender, EventArgs e) => presenter.lvw_companies_SelectedIndexChanged(GetSelectedIndices());

        // Buttons
        private void btn_AddCompany_Click(object sender, EventArgs e) => presenter.btn_AddCompany_Click();
        private void btn_DeleteCompany_Click(object sender, EventArgs e) => presenter.btn_DeleteCompany_Click();
        private void btn_EditDepartments_Click(object sender, EventArgs e) => presenter.btn_EditDepartments_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();

        //Form is being closed
        private void frm_companies_FormClosing(object sender, FormClosingEventArgs e) => presenter.frm_companies_FormClosing(e);
    }
}
