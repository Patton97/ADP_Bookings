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


        public frm_departments(int companyID)
        {
            InitializeComponent();

            //Define groups of controls
            InitialiseControlGroups();
            InitialiseChangeTracker();
            //Assign presenter
            presenter = new DepartmentPresenter(this, companyID);
        }
        private void frm_departments_Load(object sender, EventArgs e) {  /**/ }

        //Used by presenter to register itself once succesfully constructed
        public void Register(DepartmentPresenter presenter) => this.presenter = presenter;

        //Control groups allow for mass-enabling/disabling of form controls
        //This prevents unauthorised editing and helps focus user attention
        //Long-winded, slightly ugly, but very useful!
        void InitialiseControlGroups()
        {
            //Controls relating to the full department list display (left hand side)
            ctrls_DepartmentList = new List<Control>()
            {
                lbl_Departments,
                lvw_Departments,
                btn_AddDepartment
            };

            //Controls relating to the current department display (right hand side)
            ctrls_CurrentDepartment = new List<Control>()
            {
                lbl_EditDepartment,
                lbl_DepartmentID,
                txt_DepartmentID,
                lbl_DepartmentName,
                txt_DepartmentName,
                lbl_DepartmentBookings,
                lvw_DepartmentBookings,
                btn_EditBookings,
                btn_ConfirmChanges,
                btn_CancelChanges
            };
        }

        //To track if the user has begun to edit the open record,
        //  we create a custom EventHandler containing a delegate which updates a bool within the presenter
        //  then subscribe to any relevant events (TextChanged, CheckChanged, etc)
        //This proves very useful when there are several editable controls on the form (eg: Booking, Activity)
        //NOTE: Only the presenter itself can reset this to false.
        void InitialiseChangeTracker()
        {
            EventHandler handler = new EventHandler(delegate { presenter.NewChangePending(); });
            txt_DepartmentID.TextChanged += handler;
            txt_DepartmentName.TextChanged += handler;
        }

        public int[] GetSelectedIndices() => lvw_Departments.SelectedIndices.Cast<int>().ToArray();
        public int GetSelectedIndex() => GetSelectedIndices()[0];

        // Allows presenter to manually override selected indices
        public void SetSelectedIndices(int[] indices)
        {
            foreach (int index in indices)
            {
                lvw_Departments.Items[index].Selected = true;
            }
            lvw_Departments.Select();
        }
        public void SetSelectedIndex(int index) => SetSelectedIndices(new int[] { index });

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        

        // Companies ListBox - lst_companies
        private void lvw_Departments_SelectedIndexChanged(object sender, EventArgs e) => presenter.lvw_Departments_SelectedIndexChanged(GetSelectedIndices());

        // Buttons
        private void btn_AddDepartment_Click(object sender, EventArgs e) => presenter.btn_AddDepartment_Click();
        private void btn_DeleteDepartment_Click(object sender, EventArgs e) => presenter.btn_DeleteDepartment_Click();
        private void btn_EditBookings_Click(object sender, EventArgs e) => presenter.btn_EditBookings_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();

        // Form is being closed
        private void frm_departments_FormClosing(object sender, FormClosingEventArgs e) => presenter.frm_departments_FormClosing(e);
    }
}
