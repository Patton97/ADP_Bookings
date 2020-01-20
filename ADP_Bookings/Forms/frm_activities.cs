//16007006 Andrew Patton
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
    public partial class frm_activities : Form, IActivityGUI
    {
        private ActivityPresenter presenter;
        public List<Control> ctrls_ActivityList;
        public List<Control> ctrls_CurrentActivity;

        // ********************************************************************************
        // View Properties ****************************************************************
        // ******************************************************************************** 

        public string CurrentActivityID
        {
            get => txt_ActivityID.Text;
            set => txt_ActivityID.Text = value;
        }
        public string CurrentActivityName
        {
            get => txt_ActivityName.Text;
            set => txt_ActivityName.Text = value;
        }
        public decimal CurrentActivityCost
        {
            get => nud_ActivityCost.Value;
            set => nud_ActivityCost.Value = value;
        }
        public string CurrentActivityNotes
        {
            get => rtx_ActivityNotes.Text;
            set => rtx_ActivityNotes.Text = value;
        }
        public ListView.ListViewItemCollection ActivityList
        {
            get => lvw_Activities.Items;
            set => lvw_Activities.Items.AddRange(value);
        }
        public bool ActivityList_Enabled
        {
            get => ctrls_ActivityList[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_ActivityList.ForEach(ctrl => ctrl.Enabled = value);
        }
        public bool CurrentActivity_Enabled
        {
            get => ctrls_CurrentActivity[0].Enabled; //We assume if first is enabled, all are
            set => ctrls_CurrentActivity.ForEach(ctrl => ctrl.Enabled = value);
        }

        // ********************************************************************************
        // Form Functions *****************************************************************
        // ******************************************************************************** 

        public frm_activities(int bookingID)
        {
            InitializeComponent();
            InitialiseControlGroups();
            InitialiseChangeTracker();
            presenter = new ActivityPresenter(this, bookingID);
        }

        private void frm_activities_Load(object sender, EventArgs e) { /* */ }

        //Used by presenter to register itself once succesfully constructed
        public void Register(ActivityPresenter presenter) => this.presenter = presenter;

        //Control groups allow for mass-enabling/disabling of form controls
        void InitialiseControlGroups()
        {
            //Controls relating to the full activity list display (left hand side)
            ctrls_ActivityList = new List<Control>()
            {
                lbl_Activities,
                lvw_Activities,
                btn_AddActivity,
                btn_DeleteActivity,
                btn_UpdateBooking
            };

            //Controls relating to the current activity display (right hand side)
            ctrls_CurrentActivity = new List<Control>()
            {
                lbl_EditActivity,
                lbl_ActivityID,
                txt_ActivityID,
                lbl_ActivityName,
                txt_ActivityName,
                lbl_ActivityCost,
                nud_ActivityCost,
                lbl_ActivityNotes,
                rtx_ActivityNotes, 
                btn_ConfirmChanges,
                btn_CancelChanges
            };
        }

        // To track if the user has begun to edit the open record
        // NOTE: Only the presenter itself can reset this to false.
        void InitialiseChangeTracker()
        {
            EventHandler handler = new EventHandler(delegate { presenter.NewChangePending(); });
            txt_ActivityID.TextChanged += handler;
            txt_ActivityName.TextChanged += handler;
            nud_ActivityCost.ValueChanged += handler;
            rtx_ActivityNotes.TextChanged += handler;
        }

        //Encapsulates MessageBox calls in View - rather than calling it from Presenter layer
        public DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public int[] GetSelectedIndices() => lvw_Activities.SelectedIndices.Cast<int>().ToArray();
        public int GetSelectedIndex() => GetSelectedIndices()[0];

        // Allows presenter to manually override selected indices
        public void SetSelectedIndices(int[] indices) => Array.ForEach(indices, index => lvw_Activities.Items[index].Selected = true);
        public void SetSelectedIndex(int index) => SetSelectedIndices(new int[] { index });

        // Get indices of items in lvw_Activities which have been checked
        public int[] GetChosenActivities() => lvw_Activities.CheckedIndices.Cast<int>().ToArray();

        // NUD controls don't have a "FullSelect" property, this function emulates that
        void nud_ActivityCost_FullSelect() => nud_ActivityCost.Select(0, nud_ActivityCost.Text.Length);

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        
        // Activity List - new item selected
        private void lvw_Activities_SelectedIndexChanged(object sender, EventArgs e) => presenter.lvw_Activities_SelectedIndexChanged(GetSelectedIndices());

        // Activity Cost - control is selected 
        private void nud_ActivityCost_Enter(object sender, EventArgs e) => nud_ActivityCost_FullSelect();
        private void nud_ActivityCost_Click(object sender, EventArgs e) => nud_ActivityCost_FullSelect();

        // Buttons
        private void btn_AddActivity_Click(object sender, EventArgs e) => presenter.btn_AddActivity_Click();
        private void btn_UpdateBooking_Click(object sender, EventArgs e) => presenter.btn_UpdateBooking_Click();
        private void btn_DeleteActivity_Click(object sender, EventArgs e) => presenter.btn_DeleteActivity_Click();
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();

        // If the form is being closed
        private void frm_activities_FormClosing(object sender, FormClosingEventArgs e) => presenter.frm_activities_FormClosing(e);
    }
}
