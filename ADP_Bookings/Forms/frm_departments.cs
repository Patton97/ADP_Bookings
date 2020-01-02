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
    public partial class frm_departments : Form, IDepartmentGUI
    {
        private DepartmentPresenter presenter;
        public List<Control> ctrls_DepartmentList;
        public List<Control> ctrls_CurrentDepartment;

        public string CurrentDepartmentID
        {
            get => txt_DepartmentID.Text;
            set => txt_DepartmentID.Text = value;
        }
        public string CurrentDepartmentName
        {
            get => txt_DepartmentName.Text;
            set => txt_DepartmentName.Text = value;
        }
        public ListView.ListViewItemCollection DepartmentList
        {
            get => lvw_Departments.Items;
            set => lvw_Departments.Items.AddRange(value);
        }
        public ListView.ListViewItemCollection CurrentDepartmentBookings
        {
            get => lvw_DepartmentBookings.Items;
            set => lvw_DepartmentBookings.Items.AddRange(value);
        }
        public bool DepartmentList_Enabled
        {
            get => ctrls_DepartmentList[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_DepartmentList.ForEach(ctrl => ctrl.Enabled = value);
        }
        public bool CurrentDepartment_Enabled
        {
            get => ctrls_CurrentDepartment[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_CurrentDepartment.ForEach(ctrl => ctrl.Enabled = value);
        }


        public frm_departments(Company company)
        {
            InitializeComponent();
            //Define groups of controls
            InitialiseControlGroups();

            //Assign presenter
            presenter = new DepartmentPresenter(this, company);
        }

        public void Register(DepartmentPresenter presenter) => this.presenter = presenter;

        private void frm_departments_Load(object sender, EventArgs e)
        {

        }

        void InitialiseControlGroups()
        {
            //Controls relating to the full department list display (left hand side)
            ctrls_DepartmentList = new List<Control>();
            ctrls_DepartmentList.Add(lbl_Departments);
            ctrls_DepartmentList.Add(lvw_Departments);
            ctrls_DepartmentList.Add(btn_AddDepartment);

            //Controls relating to the current department display (right hand side)
            ctrls_CurrentDepartment = new List<Control>();
            ctrls_CurrentDepartment.Add(lbl_DepartmentID);
            ctrls_CurrentDepartment.Add(txt_DepartmentID);
            ctrls_CurrentDepartment.Add(lbl_DepartmentName);
            ctrls_CurrentDepartment.Add(txt_DepartmentName);
            ctrls_CurrentDepartment.Add(lvw_DepartmentBookings);
            ctrls_CurrentDepartment.Add(btn_ConfirmChanges);
            ctrls_CurrentDepartment.Add(btn_CancelChanges);
        }

        public int GetSelectedDepartmentIndex() => lvw_Departments.SelectedIndices[0];

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_companies
        private void lvw_Departments_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.lvw_Departments_SelectedIndexChanged(lvw_Departments.SelectedIndices.Cast<int>().ToArray());
        }

        // Buttons
        private void btn_AddDepartment_Click(object sender, EventArgs e) => presenter.btn_AddDepartment_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();
    }
}
