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

        public frm_activities(Booking booking, string userpath)
        {
            InitializeComponent();
            InitialiseControlGroups();
            InitialiseEventHandlers();
            presenter = new ActivityPresenter(this, booking);
        }

        private void frm_activities_Load(object sender, EventArgs e) { /* */ }

        //Used by presenter to register itself once succesfully constructed
        public void Register(ActivityPresenter presenter) => this.presenter = presenter;

        //Control groups allow for mass-enabling/disabling of form controls
        //This prevents unauthorised editing and helps focus user attention
        //Long-winded, slightly ugly, but very useful!
        void InitialiseControlGroups()
        {
            //Controls relating to the full activity list display (left hand side)
            ctrls_ActivityList = new List<Control>()
            {
                lbl_Activities,
                lvw_Activities,
                btn_AddActivity,
                //btn_DeleteActivity
            };

            //Controls relating to the current activity display (right hand side)
            ctrls_CurrentActivity = new List<Control>()
            {
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

        //To track if the user has begun to edit the open record,
        //  we create a custom EventHandler containing a delegate which updates a bool within the presenter
        //  then subscribe to any relevant events (TextChanged, CheckChanged, etc)
        //This proves very useful when there are several editable controls on the form (eg: Booking, Activity)
        //NOTE: Only the presenter itself should reset this to false.
        void InitialiseEventHandlers() //Admittedly poor function name, can't think of anything better
        {
            EventHandler handler = new EventHandler(delegate { presenter.NewChangePending(); });
            txt_ActivityID.TextChanged += handler;
            txt_ActivityName.TextChanged += handler;
            nud_ActivityCost.ValueChanged += handler;
            rtx_ActivityNotes.TextChanged += handler;
        }


        public int[] GetSelectedIndices() => lvw_Activities.SelectedIndices.Cast<int>().ToArray();
        public int GetSelectedIndex() => GetSelectedIndices()[0];

        //This function is specific to Activity as it (currently) is the only 
        //disconnected table, where updates to other records need to be sent backwards
        //As such, it looks out of place and slightly breaks the structure of the program
        public int[] GetChosenActivities() => lvw_Activities.CheckedIndices.Cast<int>().ToArray();

        // ********************************************************************************
        // Event Handlers *****************************************************************
        // ********************************************************************************        
        // Activity List - new item selected
        private void lvw_Activities_SelectedIndexChanged(object sender, EventArgs e) => presenter.lvw_Activities_SelectedIndexChanged(GetSelectedIndices());

        // Buttons
        private void btn_AddActivity_Click(object sender, EventArgs e) => presenter.btn_AddActivity_Click();
        private void btn_UpdateBooking_Click(object sender, EventArgs e) => presenter.btn_UpdateBooking_Click();
        //DeleteButton goes here
        private void btn_CancelChanges_Click(object sender, EventArgs e) => presenter.btn_CancelChanges_Click();
        private void btn_ConfirmChanges_Click(object sender, EventArgs e) => presenter.btn_ConfirmChanges_Click();

        // If the form is being closed
        private void frm_activities_FormClosing(object sender, FormClosingEventArgs e) => presenter.frm_activities_FormClosing(e);

    }
}
